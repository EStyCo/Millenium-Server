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
    public class StatsController : ControllerBase
    {
        private readonly UserStorage userStorage;
        private readonly UserRepository userRep;
        private readonly StatsService statsService;
        private readonly IMapper mapper;

        public StatsController(UserStorage _userStorage,
                               UserRepository _userRepository,
                               IMapper _mapper,
                               StatsService _statsService)
        {
            userStorage = _userStorage;
            userRep = _userRepository;
            mapper = _mapper;
            statsService = _statsService;
        }

        [HttpPost("stats")]
        public IActionResult GetStats(NameRequestDTO dto)
        {
            StatsHandler? stats = userStorage.ActiveUsers
                .Where(u => u.Name == dto.Name)
                .Select(u => u.Stats)
                .FirstOrDefault();

            if (stats == null) return BadRequest(RespFactory.ReturnBadRequest());

            return Ok(RespFactory.ReturnOk(stats.ToJson()));
        }

        [HttpPost("modifiers")]
        public IActionResult GetModifiers(NameRequestDTO dto)
        {
            var result = statsService.GetModifiers(dto);
            if (result == null) return BadRequest(RespFactory.ReturnBadRequest());
            return Ok(RespFactory.ReturnOk(result));
        }

        [HttpPut("update")]
        public async Task<IActionResult> ChangeStats(UpdateStatDTO dto)
        {
            var user = userStorage.ActiveUsers
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
