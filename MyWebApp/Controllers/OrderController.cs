using Microsoft.AspNetCore.Mvc;
using MyWebApp.Models;
using System.Text.Json;

namespace MyWebApp.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly Context _context;

        public OrderController(Context context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddNewOrder([FromBody] JsonElement orders)
        {
            var ordersToAdd = new List<Orders>();

            foreach (var body in orders.EnumerateArray())
            {

                var userId = body.GetProperty("userId").GetString();
                var quantity = body.GetProperty("quantity").GetInt32();
                var price = body.GetProperty("price").GetDecimal();
                var watchName = body.GetProperty("watchName").GetString();
                var watchBrand = body.GetProperty("watchBrand").GetString();
                var totalPrice = price * quantity;

                ordersToAdd.Add(new Orders
                {
                    OrderId = Guid.NewGuid(),
                    UserId = userId,
                    Quantity = quantity,
                    WatchName = watchName,
                    WatchBrand = watchBrand,
                    TotalPrice = totalPrice,
                    PaymentDate = DateTime.Now
                });

            }
            _context.OrdersTable.AddRange(ordersToAdd);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Orders added successfully" , ordersToAdd});

        }


        [HttpPost("myOrders")]
        public async Task<IActionResult> GetUserOrders([FromBody] JsonElement body)
        {
            string userId = body.GetProperty("userId").GetString();

            var userOrders = _context.OrdersTable
                .Where(o => o.UserId == userId)
                .ToList();

            return Ok(userOrders);
        }
    }
}

