using Microsoft.AspNetCore.Mvc;
using Server.Models.Utilities;
using Server.Models.DTO.Auth;
using Server.Services;

namespace WebApplication1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService authService;

        public AuthController(AuthService _authService)
        {
            authService = _authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(LoginRequestDTO dto)
        {
            var response = await authService.LoginUser(dto);
            if (response == null) 
            {
                return BadRequest(RespFactory.ReturnBadRequest());
            }
            return Ok(RespFactory.ReturnOk(response));
        }


        [HttpPost("reg")]
        public async Task<ActionResult> RegistrationNewUser(RegRequestDTO dto)
        {
            if(!await authService.RegistrationNewUser(dto))
            {
                return BadRequest(RespFactory.ReturnBadRequest());
            }
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
    }
}