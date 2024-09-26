using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Server.Models.DTO.User;
using Server.Models.Handlers;
using Server.Models.Handlers.Stats;
using Server.Models.Utilities;
using Server.Repository;
using Server.Services;

namespace Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StatsController(
        StatsService statsService) : ControllerBase
    {
        /// <summary>
        /// Получение значений характеристик персонажа по имени
        /// </summary>
        /// <param name="dto">123</param>
        [HttpPost("stats")]
        public IActionResult GetStats(NameRequestDTO dto)
        {
            var stats = statsService.GetStats(dto);
            if(stats == null)
                return BadRequest(RespFactory.ReturnBadRequest());
            return Ok(RespFactory.ReturnOk(stats));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("modifiers")]
        public IActionResult GetModifiers(NameRequestDTO dto)
        {
            var result = statsService.GetModifiers(dto);
            if (result == null) 
                return BadRequest(RespFactory.ReturnBadRequest());
            return Ok(RespFactory.ReturnOk(result));
        }
        /// <summary>
        /// 1111
        /// </summary>
        [HttpPut("update")]
        public async Task<IActionResult> ChangeStats(UpdateStatDTO dto)
        {
            return BadRequest();
            /*var user = userStorage.ActiveUsers
                .Where(x => x.Name == dto.Name)
                .FirstOrDefault();

            if (user == null || !await userRep.CharacterExists(dto.Name)) return BadRequest(RespFactory.ReturnBadRequest());

            await userRep.UpdateStats(dto);

            var newCounts = await userRep.GetStats(dto.Name);
            if (newCounts != null)
            {
                user.Stats.SetStats(newCounts);
                user.ReAssembly();
            }
            return Ok(RespFactory.ReturnOk());*/
        }

        [HttpPost("states")]
        public IActionResult GetStates(NameRequestDTO dto)
        {
            var states = statsService.GetStates(dto);
            if(states == null)
                return BadRequest(RespFactory.ReturnBadRequest());
            return Ok(RespFactory.ReturnOk(new CustomList<StateDTO>(states)));
        }
    }
}
