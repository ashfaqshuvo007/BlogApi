using BlogApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenerateTokensController(IConfiguration configuration) : ControllerBase
    {
        private readonly IConfiguration _config = configuration;

        [HttpPost]
        public IActionResult Authenticate(Login loginRequest)
        {
            if (loginRequest.Username == "Admin" && loginRequest.Password == "Password") 
            {
                var issuer = _config["JWT:Issuer"];
                var audience = _config["JWT:Audience"];
                var _key = Encoding.ASCII.GetBytes(_config["JWT:Key"]!);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim("Id", Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Sub, loginRequest.Username),
                        new Claim(JwtRegisteredClaimNames.Email, loginRequest.Username),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())

                    }),

                    Expires = DateTime.UtcNow.AddMinutes(10),
                    Issuer = issuer,
                    Audience = audience,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_key), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var stringToken = tokenHandler.WriteToken(token);
                return Ok(stringToken);
            }
            return Unauthorized("You are not authorized to access!");
        }
    }
}
