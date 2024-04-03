using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server;
using Server.Models;
using Server.Models.DTO.Auth;
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
        protected APIResponse response;

        public AuthController(UserRepository _userRep, UserStorage _userStorage)
        {
            userRep = _userRep;
            userStorage = _userStorage;
            response = new();
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginRequestDTO user)
        {
            var userResponse = await userRep.LoginUser(user.Email, user.Password);

            if (userResponse == null)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.IsSuccess = false;
                response.ErrorMessages.Add("Username or password is incorrect!");

                return BadRequest(response);
            }

            Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss")}] {userResponse.Character.CharacterName} Авторизован.");

            response.StatusCode = HttpStatusCode.OK;
            response.IsSuccess = true;
            response.Result = userResponse;
            return Ok(response);
        }

        [HttpPost("reg")]
        public async Task<ActionResult> AddUser([FromBody] RegRequestDTO dto)
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
