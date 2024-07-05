using Microsoft.AspNetCore.Mvc;
using Server;
using Server.Models;
using Server.Models.DTO.Auth;
using Server.Models.Utilities;
using Server.Repository;
using System.Net;

namespace WebApplication1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserRepository userRep;
        private readonly UserStorage userStorage;

        public AuthController(UserRepository _userRep, UserStorage _userStorage)
        {
            userRep = _userRep;
            userStorage = _userStorage;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(LoginRequestDTO dto)
        {
            var userResponse = await userRep.LoginUser(dto.Email, dto.Password);
            if (userResponse == null) return BadRequest(RespFactory.ReturnBadRequest());

            var character = await userRep.GetCharacter(userResponse.Character.Name);
            var stats = await userRep.GetStats(userResponse.Character.Name);

            if (character != null && stats != null)
            {
                userStorage.AddActiveUser(stats, character);
            }

            Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss")}] {userResponse.Character.Name} Авторизован.");
            return Ok(RespFactory.ReturnOk(userResponse));
        }


        [HttpPost("reg")]
        public async Task<ActionResult> AddUser(RegRequestDTO dto)
        {
            if (!await userRep.IsUniqueUser(dto))
                return BadRequest(RespFactory.ReturnBadRequest());
            if (!await userRep.Registration(dto))
                return BadRequest(RespFactory.ReturnBadRequest());

            return Ok(RespFactory.ReturnOk());
        }

        [HttpGet("version")]
        public IActionResult CheckVersion()
        {
            var dictionary = new Dictionary<string, string>
            {
                { "version", ActualVersion.Version }
            };

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