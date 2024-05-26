using Microsoft.AspNetCore.Mvc;
using Server.Models.DTO;
using Server.Models.Utilities;
using Server.Repository;

namespace Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TravelController : ControllerBase
    {
        private readonly TravelRepository rep;
        private readonly UserStorage userStorage;

        public TravelController(TravelRepository _rep, UserStorage _userStorage)
        {
            rep = _rep;
            userStorage = _userStorage;
        }

        [HttpPost("get")]
        public async Task<IActionResult> GetArea(NameRequestDTO dto)
        {
            var travelResponse = await rep.GetArea(dto);

            if (travelResponse == null)
            {
                return BadRequest(RespFactory.ReturnBadRequest());
            }
            return Ok(RespFactory.ReturnOk(travelResponse));
        }

        [HttpPost("go")]
        public async Task<IActionResult> GoNewArea(TravelDTO dto)
        {
            var currentArea = rep.GetArea(new(dto.Name));
            var travelResponse = await rep.GoNewArea(dto);

            var user = userStorage.ActiveUsers.Where(x => x.Name == dto.Name).FirstOrDefault();

            if (travelResponse != null && user != null)
            {
                user.Place = travelResponse.Place;
                return Ok(RespFactory.ReturnOk());
            }

            return BadRequest(RespFactory.ReturnBadRequest());
        }
    }
}
