using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Hubs;
using Server.Models;
using Server.Models.DTO;
using Server.Repository;
using System.Net;

namespace Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TravelController : ControllerBase
    {
        private readonly TravelRepository rep;
        private readonly UserStorage userStorage;
        protected APIResponse response;

        public TravelController(TravelRepository _rep, UserStorage _userStorage)
        {
            rep = _rep;
            userStorage = _userStorage;
            response = new();
        }

        [HttpPost("get")]
        public async Task<IActionResult> GetArea([FromBody] TravelDTO travel)
        {
            var travelResponse = await rep.GetArea(travel);

            if (travelResponse == null)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.IsSuccess = false;
                response.ErrorMessages.Add("Персонаж не найден!");

                return BadRequest(response);
            }

            response.StatusCode = HttpStatusCode.OK;
            response.IsSuccess = true;
            response.Result = travelResponse;
            return Ok(response);
        }

        [HttpPost("go")]
        public async Task<IActionResult> GoNewArea([FromBody] TravelDTO travel)
        {
            var travelResponse = await rep.GoNewArea(travel);

            if (travelResponse == null)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.IsSuccess = false;
                response.ErrorMessages.Add("Движение невозможно!");

                return BadRequest(response);
            }

            response.StatusCode = HttpStatusCode.OK;
            response.IsSuccess = true;
            response.Result = true;
            return Ok(response);
        }

        [HttpPost("break")]
        public async Task<IActionResult> BreakChar([FromBody] string name)
        {
            await userStorage.BreakCharacter(name);

            response.StatusCode = HttpStatusCode.OK;
            response.IsSuccess = true;
            return Ok(response);
        }
    }
}
