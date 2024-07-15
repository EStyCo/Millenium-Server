using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Server.Models.DTO.User;
using Server.Models.Handlers.Stats;
using Server.Models.Utilities;
using Server.Repository;

namespace Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StatsController : ControllerBase
    {
        private readonly UserStorage userStorage;
        private readonly UserRepository userRep;
        private readonly IMapper mapper;

        public StatsController(UserStorage _userStorage, 
                               UserRepository _userRepository, 
                               IMapper _mapper)
        {
            userStorage = _userStorage;
            userRep = _userRepository;
            mapper = _mapper;
        }

        [HttpPost("get")]
        public async Task<IActionResult> GetStats(NameRequestDTO dto)
        {
            StatsHandler? stats = userStorage.ActiveUsers
                .Where(u => u.Name == dto.Name)
                .Select(u => u.Stats)
                .FirstOrDefault();

            if (stats == null) return BadRequest(RespFactory.ReturnBadRequest());

            return Ok(RespFactory.ReturnOk(mapper.Map<StatDTO>(stats)));
        }

        [HttpPut("update")]
        public async Task<IActionResult> ChangeStats(UpdateStatDTO dto)
        {
            var stats = userStorage.ActiveUsers
                .Where(x => x.Name == dto.Name)
                .Select(x => x.Stats)
                .FirstOrDefault();

            if (stats == null || !await userRep.UserExists(dto.Name)) return BadRequest(RespFactory.ReturnBadRequest());

            await userRep.UpdateStats(dto);

            var newCounts = await userRep.GetStats(dto.Name);
            if (newCounts != null)
            {
                var s = stats as UserStatsHandler;
                s?.CreateStats(newCounts);
            }

           // await userStorage.ChangeStats(dto);
            return Ok(RespFactory.ReturnOk());
        }

        [HttpPost("states")]
        public async Task<IActionResult> GetStates(NameRequestDTO dto)
        {
            var states = userStorage.ActiveUsers
                .Where(u => u.Name == dto.Name)
                .SelectMany(u => u.States.Keys)
                .Select(x => x.ToJson())
                .ToList();

            return Ok(RespFactory.ReturnOk(new CustomList<StateDTO>(states)));
        }
    }
}
