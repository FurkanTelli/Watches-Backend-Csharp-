using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebApp.Models;

namespace MyWebApp.Controllers
{
    [Route("api/watches")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly Context _context;

        public ProductController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var allWatches = await _context.WatchesTable.ToListAsync();

            return Ok(allWatches);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var watch = await _context.WatchesTable.FindAsync(id);
            if(watch == null) return NotFound();    
            return Ok(watch);
        }


    }
}
