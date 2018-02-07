using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtSecurity.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IConfiguration _config;

        public AuthController(IConfiguration configuration)
        {
            _config = configuration;
        }

        [Route("login")]
        [HttpGet]
        [HttpPost]
        public IActionResult Login(string name, string role)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest();
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, name),
                new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new ContentResult() { Content = new JwtSecurityTokenHandler().WriteToken(token) };
        }

        [Route("logout")]
        [HttpGet]
        [HttpPost]
        public IActionResult Logout()
        {
            return new ContentResult() { Content = "Clean JWT at front-end, like browser local storage or else" };
        }
    }
}
