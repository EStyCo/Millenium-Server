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
            {
                return BadRequest(RespFactory.ReturnBadRequest());
            }

            var userResponse = await userRep.Registration(dto);

            if (!userResponse)
            {
                return BadRequest(RespFactory.ReturnBadRequest());
            }

            //RegResponseDTO result = new(){ Name = dto.CharacterName, IsSuccess = userResponse };

            return Ok(RespFactory
                  .ReturnOk());
        }
    }
}
