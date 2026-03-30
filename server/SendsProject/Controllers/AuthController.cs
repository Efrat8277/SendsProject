using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SendsProject.Core.Models.Classes;
using SendsProject.Core.Services;
using SendsProject.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SendsProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        public AuthController(IConfiguration configuration,IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginPostModel loginPostModel)
        {
            var user = await _userService.GetByUserNameAsync(loginPostModel.UserName,loginPostModel.Password);
           if(user!=null) 
            {
                var claims=new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                };
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokeOptions = new JwtSecurityToken(
                    issuer: _configuration["JWT:Issuer"],
                    audience: _configuration["JWT:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddHours(6),
                    signingCredentials: signinCredentials
                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return Ok(new { Token = tokenString });
            }
            return Unauthorized();
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User userModel)
        {
            // בדיקה בסיסית אם המשתמש כבר קיים (מומלץ)
            var existingUser = await _userService.GetByUserNameAsync(userModel.UserName, userModel.Password);
            if (existingUser != null)
            {
                return BadRequest(new { message = "שם המשתמש כבר תפוס" });
            }

            // הוספת המשתמש דרך הסרביס
            // הערה: וודאי שב-IUserService יש מתודת Add או Create
            var newUser = await _userService.AddUserAsync(userModel);

            if (newUser == null) return BadRequest();

            return Ok(new { message = "ההרשמה בוצעה בהצלחה" });
        }

    }
}
