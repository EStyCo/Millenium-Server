using Server.Hubs.Locations.Interfaces;
using Server.Models.Spells.States;
using Microsoft.AspNetCore.Mvc;
using Server.Models.Utilities;
using Server.Services;
using Server.Hubs.DTO;
using Server.Hubs;

namespace Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CombatController(
        CombatService combatService,
        UserStorage userStorage,
        PlaceService placeService) : ControllerBase
    {
        private static readonly SemaphoreSlim MonsterSemaphore = new(1, 1);
        private static readonly SemaphoreSlim UserSemaphore = new(1, 1);
        private static readonly SemaphoreSlim SelfSemaphore = new(1, 1);

        [HttpPost("Monster")]
        public async Task<IActionResult> AttackMonster(AttackMonsterDTO dto)
        {
            await MonsterSemaphore.WaitAsync();
            try
            {
                await combatService.AttackMonster(dto);
            }
            finally
            {
                MonsterSemaphore.Release();
            }

            return Ok(RespFactory.ReturnOk());
        }

        [HttpPost("User")]
        public IActionResult AttackUser(AttackUserDTO dto)
        {
            var user = userStorage.GetUser(dto.NameUser);
            var target = userStorage.GetUser(dto.NameTarget);

            if (user != null && user.CanAttack &&
                target != null && !target.States.Keys.OfType<WeaknessState>().Any())
            {
                (placeService.GetPlace(dto.Place) as IBattleUsers)?.AttackUser(user, target, dto.Type);
            }

            return Ok(RespFactory.ReturnOk());
        }

        [HttpPost("Self")]
        public async Task<IActionResult> UseSelfSpell(UseSelfSpellDTO dto)
        {
            await SelfSemaphore.WaitAsync();
            try
            {
                combatService.UseSelfSpell(dto);
            }
            finally
            {
                SelfSemaphore.Release();
            }
            return Ok(RespFactory.ReturnOk());
        }
    }
}