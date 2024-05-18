using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server;
using Server.Models;
using Server.Models.DTO.Auth;
using Server.Models.Utilities;
using Server.Repository;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;

namespace WebApplication1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserRepository userRep;
        private readonly UserStorage userStorage;
        protected APIResponse response;

        public AuthController(UserRepository _userRep, UserStorage _userStorage)
        {
            userRep = _userRep;
            userStorage = _userStorage;
            response = new();
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
            /*bool ifUserNameUnique = await userRep.IsUniqueUser( user.Email);
            if (!ifUserNameUnique)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.IsSuccess = false;
                response.ErrorMessages.Add("Данные Имя/Эл.почта уже заняты!");
                return BadRequest(response);
            }*/

            var userResponse = await userRep.Registration(dto);

            if (!userResponse)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.IsSuccess = false;
                response.ErrorMessages.Add("Ошибка регистрации");

                return BadRequest(response);
            }

            //RegResponseDTO result = new(){ Name = dto.CharacterName, IsSuccess = userResponse };

            response.StatusCode = HttpStatusCode.OK;
            response.IsSuccess = true;
            response.Result = "Пользователь успешно добавлен!";
            return Ok(response);
        }
    }
}
