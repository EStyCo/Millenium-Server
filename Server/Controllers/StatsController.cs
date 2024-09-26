using Microsoft.AspNetCore.Mvc;
using Server.Models.Utilities;
using Server.Models.DTO.User;
using Server.Services;

namespace Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StatsController(
        StatsService statsService) : ControllerBase
    {
        [HttpPost("stats")]
        public IActionResult GetStats(NameRequestDTO dto)
        {
            var stats = statsService.GetStats(dto);
            if (stats == null)
                return BadRequest(RespFactory.ReturnBadRequest());
            return Ok(RespFactory.ReturnOk(stats));
        }

        [HttpPost("modifiers")]
        public IActionResult GetModifiers(NameRequestDTO dto)
        {
            var result = statsService.GetModifiers(dto);
            if (result == null)
                return BadRequest(RespFactory.ReturnBadRequest());
            return Ok(RespFactory.ReturnOk(result));
        }

        [HttpPut("update")]
        public async Task<IActionResult> ChangeStats(UpdateStatDTO dto)
        {
            if (await statsService.UpdateStats(dto))
                return Ok(RespFactory.ReturnOk());
            return BadRequest(RespFactory.ReturnBadRequest());
        }

        [HttpPost("states")]
        public IActionResult GetStates(NameRequestDTO dto)
        {
            var states = statsService.GetStates(dto);
            if (states == null)
                return BadRequest(RespFactory.ReturnBadRequest());
            return Ok(RespFactory.ReturnOk(new CustomList<StateDTO>(states)));
        }
    }
}
