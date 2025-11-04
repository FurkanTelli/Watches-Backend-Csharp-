using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebApp.Models;

namespace MyWebApp.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly Context _context;


        public UserController(Context context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> GetUser([FromBody] dynamic body)
        {

            string userName = body.GetProperty("userName").GetString();
            string email = body.GetProperty("email").GetString();
            string password = body.GetProperty("password").GetString();

            //var user = await _context.UsersTable
            //    .FirstOrDefaultAsync(u =>
            //        u.UserName == userName &&
            //        u.UserEmail == email &&
            //        u.UserPassword == password
            //    );

            var user = await _context.UsersTable
           .Where(u => u.UserName == userName && u.UserEmail == email && u.UserPassword == password)
           .Select(u => new
           {
               u.UserId,
               u.UserName,
               u.UserEmail
           })
           .FirstOrDefaultAsync();


            if (user == null)
                return Unauthorized("Kullanıcı adı, email veya şifre yanlış.");

            return Ok(new
            {
                user.UserId,
                user.UserName,
                user.UserEmail
            });
        }

    }
}
