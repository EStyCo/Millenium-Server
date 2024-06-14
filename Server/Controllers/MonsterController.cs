using Microsoft.AspNetCore.Mvc;
using Server.Models.DTO;
using Server.Models.Interfaces;
using Server.Models.Utilities;
using Server.Repository;

namespace Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MonsterController : ControllerBase
    {
        private readonly UserStorage userStorage;
        private readonly IAreaStorage areaStorage;
        private readonly UserRepository userRepository;

        public MonsterController(UserStorage _userStorage,
                                 IAreaStorage _areaStorage,
                                 UserRepository _userRepository)
        {
            userStorage = _userStorage;
            areaStorage = _areaStorage;
            userRepository = _userRepository;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddMonster(PlaceDTO dto)
        {
            areaStorage.GetBattlePlace(dto.Place)?.AddMonster();

            return Ok(RespFactory.ReturnOk());
        }

        [HttpPost("attack")]
        public async Task<IActionResult> AttackMonster(AttackMonsterDTO dto)
        {
            var user = userStorage.ActiveUsers.FirstOrDefault(x => x.Name == dto.Name);

            if (user != null && user.CanAttack)
            {
                int addingExp = areaStorage.GetBattlePlace(dto.Place).AttackMonster(dto, user);

                if (addingExp > 0)
                {
                    userStorage.AddExp(new UpdateExpDTO { Name = dto.Name, Exp = addingExp });
                    await userRepository.UpdateExp(addingExp, user.Name);
                }
            }

            return Ok(RespFactory.ReturnOk());
        }

        [HttpPost("getMonsters")]
        public async Task<IActionResult> GetMonsters(PlaceDTO dto)
        {
            var monsters = areaStorage.GetBattlePlace(dto.Place).Monsters ?? new();

            return Ok(RespFactory.ReturnOk(monsters));
        }
    }
}
