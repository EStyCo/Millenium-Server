using Microsoft.AspNetCore.Mvc;
using Server.Hubs.Locations;
using Server.Models.DTO;
using Server.Models.Interfaces;
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
        private readonly IAreaStorage areaStorage;

        public TravelController(TravelRepository _rep, UserStorage _userStorage, IAreaStorage _areaStorage)
        {
            rep = _rep;
            userStorage = _userStorage;
            areaStorage = _areaStorage;
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

            var stats = userStorage.ActiveUsers.Where(x => x.Name == dto.Name)
                .Select(x => x.Stats)
                .FirstOrDefault();

            if (travelResponse == null || stats == null)
            {
                return BadRequest(RespFactory.ReturnBadRequest());
            }

            /*await areaStorage.GetPlace(currentArea.Result.Place)?.LeavePlace(dto.Name);
            await areaStorage.GetPlace(dto.Place)?.EnterPlace(dto.Name, stats.Level);*/

            return Ok(RespFactory.ReturnOk());
        }
    }
}
