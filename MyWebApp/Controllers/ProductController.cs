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
        // The Context object we will use in database operations (comes with dependency injection)
        private readonly Context _context;



        // Constructor — Context is automatically injected when creating the Controller
        public ProductController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {

            // We retrieve all records from the database asynchronously
            var allWatches = await _context.WatchesTable.ToListAsync();



            // Return all records with 200 OK
            return Ok(allWatches);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {

            /* We are searching for a record in the database based on the GUID value 
            that comes with the id parameter. */
            var watch = await _context.WatchesTable.FindAsync(id);


            // If no record is found we return 404 Not Found
            if (watch == null) return NotFound();

            // Return the found record with 200 OK
            return Ok(watch);
        }

        [HttpPost]

        public async Task<IActionResult> AddNewWatch([FromBody] dynamic body)
        {
            // [FromBody] → Retrieves data from the body section as JSON.
            // newWatch → Data from the Frontend or Postman is stored here.
            Console.WriteLine(body);
            var newWatch = new Watch
            {
                Id = Guid.NewGuid(),
                WatchName = body.GetProperty("watchname").GetString(),
                WatchBrand = body.GetProperty("watchbrand").GetString(),
                Price = body.GetProperty("price").GetDecimal(),
                Img = body.GetProperty("imgAdress").GetString() 
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
