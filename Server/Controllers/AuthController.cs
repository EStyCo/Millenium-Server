using Microsoft.AspNetCore.Mvc;
using Server.Models.Utilities;
using Server.Models.DTO.Auth;
using Server.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController(
        AuthService authService) : ControllerBase
    {

        [HttpPost("loginByEmail")]
        public async Task<IActionResult> LoginUserByEmail(EmailLoginRequest dto)
        {
            var response = await authService.LoginUserByEmail(dto);
            if (response == null)
                return BadRequest(RespFactory.ReturnBadRequest());
            return Ok(RespFactory.ReturnOk(response));
        }

        [HttpPost("loginByGoogle")]
        public async Task<IActionResult> LoginUserByGoogle(GoogleLoginRequest dto)
        {
            /*var response = await authService.LoginUser(dto);
            if (response == null)
            {
                return BadRequest(RespFactory.ReturnBadRequest());
            }*/
            return Ok(RespFactory.ReturnOk());
        }


        [HttpPost("reg")]
        public async Task<ActionResult> RegistrationNewUser(RegRequestDTO dto)
        {
            if (!await authService.RegistrationNewUser(dto))
                return BadRequest(RespFactory.ReturnBadRequest());
            return Ok(RespFactory.ReturnOk());
        }

        [HttpGet("version")]
        public IActionResult CheckVersion()
        {
            Dictionary<string, string> dictionary = new()
            { { "version", ActualVersion.Version } };
            return Ok(RespFactory.ReturnOk(dictionary));
        }

        [HttpPost("changeVersion")]
        public IActionResult ChangeVersion(string version)
        {
            ActualVersion.Version = version;
            return RedirectToAction(nameof(CheckVersion));
        }

        [HttpPost("auth")]
        public IActionResult AuthUser(string name)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("-very-very-very-very-very-very-very-veryvery-veryvery-veryvery-veryvery-veryvery-very");

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
                [
                    new Claim(ClaimTypes.Name, name),
                ]),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return Ok(RespFactory.ReturnOk(tokenString));
        }

        [HttpGet("testAuth")]
        [Authorize]
        public IActionResult TestAuth()
        {
            DateTime now = DateTime.Now;
            DateTime utcNow = DateTime.UtcNow;

            Console.WriteLine("Local Time (DateTime.Now): " + now);
            Console.WriteLine("UTC Time (DateTime.UtcNow): " + utcNow);

            return Ok(RespFactory.ReturnOk($"{User.FindFirst(ClaimTypes.Name)?.Value} Вы авторизированы!"));
        }
    }
}