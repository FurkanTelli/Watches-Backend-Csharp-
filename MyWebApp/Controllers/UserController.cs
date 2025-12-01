using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyWebApp.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

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


            var user = await _context.UsersTable
                .FirstOrDefaultAsync(u =>
                u.UserName == userName &&
                u.UserEmail == email &&
                u.UserPassword == password);


            if (user == null) return Unauthorized("Username or password is incorrect");

            var generateToken = GenerateJwtToken(user.UserId.ToString(), user.UserName, user.UserEmail);

            return Ok(
                new
                {
                    generateToken,
                    user = new { user.UserId, user.UserName, user.UserEmail }
                });

        }



        [HttpPost("createUser")]
        public async Task<IActionResult> CreateUser([FromBody] JsonElement body)
        {

            Console.WriteLine(body);
            var email = body.GetProperty("email").GetString();
            var userName = body.GetProperty("userName").GetString();
            var password = body.GetProperty("password").GetString();


            var isEmailExist = await _context.UsersTable.FirstOrDefaultAsync(u => u.UserEmail == email);
            var isUsernameExist = await _context.UsersTable.FirstOrDefaultAsync(u => u.UserName == userName);

            if (isEmailExist != null)
            {
                return BadRequest("Email is already in use");
            }

            if (isUsernameExist != null)
            {
                return BadRequest("Username is already in use");
            }

            var newUser = new User
            {
                UserId = Guid.NewGuid(),
                UserName = userName,
                UserEmail = email,
                UserPassword = password,
            };


            _context.Add(newUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new {id = newUser.UserId }, newUser);
        }


        [HttpPut("updateUser/{id:guid}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] dynamic body)
        {
            var match = await _context.UsersTable.FindAsync(id);
            if (match == null) return NotFound();


            match.UserName = body.GetProperty("userName").GetString();
            match.UserEmail = body.GetProperty("userEmail").GetString();
            match.UserPassword = body.GetProperty("password").GetString();

            await _context.SaveChangesAsync();

            return Ok(match);
        }


        [HttpPost("logout")]
        public async Task<IActionResult> LogOut()
        {
            return Ok(new { message = "Logged out successfully" });
        }



        [HttpDelete("deleteUser/{id:guid}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var match = await _context.UsersTable.FindAsync(id);

            if (match == null) return NotFound();

            _context.UsersTable.Remove(match);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
