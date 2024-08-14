using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NadinSoftProject.Data;
using NadinSoftProject.Models;
using NadinSoftProject.Models.Dto;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NadinSoftProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _configuration;
        public AuthController(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }

        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<User> Register(UserDto userDto)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(userDto.PasswordHash);
            var user = new User()
            {
                UserName = userDto.UserName,
                PasswordHash = passwordHash
            };
            _db.Users.Add(user);
            _db.SaveChanges();
            return Ok(user);
        }
        [HttpPost("Login")]
        public ActionResult<User> Login(UserDto userDto)
        {
            var user = _db.Users.FirstOrDefault(u => u.Id == userDto.Id);
            if(user == null || user.UserName != userDto.UserName)
            {
                ModelState.AddModelError("CustomError", "User Not Found");
                return BadRequest(ModelState);
            }
            if (!BCrypt.Net.BCrypt.Verify(userDto.PasswordHash, user.PasswordHash))
            {
                ModelState.AddModelError("CustomError", "Wrong Password");
                return BadRequest(ModelState);
            }
            string token = CreateToken(user);
            return Ok(token);
        }
        [HttpGet("GetCurrentUser"), Authorize]
        public IActionResult GetCurrentUser()
        {
            return Ok(new {UserName = User.FindFirstValue(ClaimTypes.Name) });
        }
        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName) 
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}
