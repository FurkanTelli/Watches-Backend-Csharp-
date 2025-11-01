using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebApp.Models;
using System.Runtime.InteropServices;

namespace MyWebApp.Controllers
{
    [Route("api/watches")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        // Veritabanı işlemlerinde kullanacağımız Context nesnesi (dependency injection ile geliyor)
        private readonly Context _context;



        // Constructor — Controller oluşturulurken Context otomatik olarak enjekte edilir
        public ProductController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {

            // Tüm kayıtları veritabanından asenkron şekilde alıyoruz
            var allWatches = await _context.WatchesTable.ToListAsync();



            // 200 OK cevabı ile birlikte verileri döndürüyoruz
            return Ok(allWatches);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {

            // id parametresiyle gelen GUID değerine göre veritabanından kayıt arıyoruz
            var watch = await _context.WatchesTable.FindAsync(id);


            // Eğer kayıt bulunmazsa 404 Not Found döneriz
            if (watch == null) return NotFound();

            // Bulunan kaydı 200 OK ile geri 
            return Ok(watch);
        }

        [HttpPost]

        public async Task<IActionResult> AddNewWatch([FromBody] dynamic body)
        {
            // [FromBody] → Gövde (body) kısmından JSON olarak veri alır
            // newWatch → Frontend veya Postman'den gelen veri burada tutulur
            Console.WriteLine(body);
            var newWatch = new Watch
            {
                Id = Guid.NewGuid(),
                WatchName = body.GetProperty("watchname").GetString(),
                WatchBrand = body.GetProperty("watchbrand").GetString(),
                Price = body.GetProperty("price").GetDecimal(),
                Img = body.GetProperty("imgAdress").GetString() // JSON’daki alan adıyla eşleşiyor 👈
            };

            await _context.WatchesTable.AddAsync(newWatch);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = newWatch.Id }, newWatch);
        }


        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateTheWatch(Guid id, [FromBody] dynamic body)
        {
            var watch = await _context.WatchesTable.FindAsync(id);
            if (watch == null) return NotFound();

            watch.WatchName = body.GetProperty("watchname").GetString();
            watch.WatchBrand = body.GetProperty("watchbrand").GetString();
            watch.Price = body.GetProperty("price").GetDecimal();
            watch.Img = body.GetProperty("imgAdress").GetString();

            await _context.SaveChangesAsync();

            return Ok(watch);
            
        }


    }
}
