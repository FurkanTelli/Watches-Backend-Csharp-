using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyWebApp.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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

        // -------------------------------------------------------------------------------------------
        // JWT Token Generation Method
        private string GenerateJwtToken(string userId, string userName, string userEmail)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("gD7!mA92kL*zQp@X4rT9wS1bC6fV0hN3"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.Email, userEmail),
                new Claim(JwtRegisteredClaimNames.Iss, "https://localhost")

            };

            var myToken = new JwtSecurityToken(
                issuer: "http://localhost",
                audience: null,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(myToken);
        }

        // -------------------------------------------------------------------------------------------


        [HttpPost("login")]
        public async Task<IActionResult> GetUser([FromBody] dynamic body)
        {

            string userName = body.GetProperty("userName").GetString();
            string email = body.GetProperty("email").GetString();
            string password = body.GetProperty("password").GetString();



            var user = await _context.UsersTable.Where(u => u.UserName == userName && u.UserEmail == email && u.UserPassword == password)
           .Select(u => new
           {
               u.UserId,
               u.UserName,
               u.UserEmail
           })
           .FirstOrDefaultAsync();


            if (user == null) return Unauthorized("Username or password is incorrect");

            var generateToken = GenerateJwtToken(user.UserId.ToString(), user.UserName, user.UserEmail);

            return Ok(
                new {generateToken, user = new {user.UserId,user.UserName,user.UserEmail}
            });

        }


        [HttpPost("logout")]
        public async Task<IActionResult> LogOut()
        {
            return Ok(new { message = "Logged out successfully" });
        }

    }
}
