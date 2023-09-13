using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DemoNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class oauthController : ControllerBase
    {
        private readonly string _secretKey = "YFR5zeptyqeDZikGQxIEwo1qpipV7UqR";
        private readonly string _issuer;
        private readonly string _audience;
        [HttpPost]
        public ActionResult Login([FromBody] LoginRequestModel model)
        {
            // Thực hiện xác thực và kiểm tra thông tin đăng nhập ở đây
            if (model.Username == "admin" && model.Password == "password")
            {
                // Xác thực thành công
                var token = GenerateToken(model.Username); // Hàm tạo token
                return Ok(new { Token = token });
            }

            // Xác thực không thành công
            return Unauthorized();
        }

        private string GenerateToken(string username)
        {
            var claims = new[]
                       {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, "admin"),
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _issuer,
                Audience = _audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }

    public class LoginRequestModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
