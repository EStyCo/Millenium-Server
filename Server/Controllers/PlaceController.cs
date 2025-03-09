using Server.Models.Entities.Monsters.DTO;
using Server.Hubs.Locations.Interfaces;
using Server.Models.Spells.States;
using Microsoft.AspNetCore.Mvc;
using Server.Models.Utilities;
using Server.Models.DTO.User;
using Server.Hubs.DTO;
using Server.Services;
using Server.Hubs;

namespace Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PlaceController(
        UserStorage userStorage,
        PlaceService placeService) : ControllerBase
    {
        [HttpPost("add")]
        public IActionResult AddMonster(PlaceDTO dto)
        {
            placeService.GetBattlePlace(dto.Place)?.AddMonster();

            return Ok(RespFactory.ReturnOk());
        }

        [HttpPost("attackUser")]
        public IActionResult AttackUser(AttackUserDTO dto)
        {
            var user = userStorage.ActiveUsers.FirstOrDefault(x => x.Name == dto.NameUser);
            var target = userStorage.ActiveUsers.FirstOrDefault(x => x.Name == dto.NameTarget);

            if (user != null && user.CanAttack &&
                target != null && !target.States.Keys.OfType<WeaknessState>().Any())
            {
                (placeService.GetPlace(dto.Place) as IBattleUsers)?.AttackUser(user, target, dto.Type);
            }

            return Ok(RespFactory.ReturnOk());
        }

        [HttpPost("getDetailsMonster")]
        public IActionResult GetDetailsMonster(DetailsMonsterRequest dto)
        {
            var details = placeService.GetBattlePlace(dto.Place)?.Monsters.FirstOrDefault(x => x.Id == dto.Id)
                ?.DetailsToJson();
            if (details == null) return BadRequest(RespFactory.ReturnBadRequest());

            return Ok(RespFactory.ReturnOk(details));
        }

        [HttpPost("getMonsters")]
        public IActionResult GetMonsters(PlaceDTO dto)
        {
            var monsters = placeService.GetBattlePlace(dto.Place)?.Monsters.Select(x => x.ToJson()).ToList() ?? new();

            return Ok(RespFactory.ReturnOk(monsters));
        }
    }
}