﻿using Microsoft.AspNetCore.Mvc;
using Server.Hubs.DTO;
using Server.Hubs.Locations.Interfaces;
using Server.Models;
using Server.Models.DTO.User;
using Server.Models.Handlers.Stats;
using Server.Models.Interfaces;
using Server.Models.Monsters.DTO;
using Server.Models.Spells.States;
using Server.Models.Utilities;
using Server.Repository;
using Server.Services;

namespace Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PlaceController : ControllerBase
    {
        private readonly UserStorage userStorage;
        private readonly PlaceService placeService;
        private readonly UserRepository userRepository;

        public PlaceController(UserStorage _userStorage,
                               PlaceService _placeService,
                               UserRepository _userRepository)
        {
            userStorage = _userStorage;
            placeService = _placeService;
            userRepository = _userRepository;
        }

        [HttpPost("add")]
        public IActionResult AddMonster(PlaceDTO dto)
        {
            placeService.GetBattlePlace(dto.Place)?.AddMonster();

            return Ok(RespFactory.ReturnOk());
        }

        [HttpPost("attackMonster")]
        public async Task<IActionResult> AttackMonster(AttackMonsterDTO dto)
        {
            var user = userStorage.ActiveUsers.FirstOrDefault(x => x.Name == dto.Name);

            if (user != null && user.CanAttack)
            {
                int addingExp = placeService.GetBattlePlace(dto.Place).AttackMonster(dto, user);

                if (addingExp > 0)
                {
                    (user.Stats as UserStatsHandler)?.AddExp(addingExp);
                    await userRepository.UpdateExp(addingExp, user.Name);
                }
            }

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
            var details = placeService.GetBattlePlace(dto.Place)?.Monsters.FirstOrDefault(x => x.Id == dto.Id)?.DetailsToJson();
            if (details == null) return BadRequest(RespFactory.ReturnBadRequest());

            return Ok(RespFactory.ReturnOk(details));
        }

        [HttpPost("getMonsters")]
        public IActionResult GetMonsters(PlaceDTO dto)
        {
            var monsters = placeService.GetBattlePlace(dto.Place)?.Monsters.Select(x =>x.ToJson()).ToList() ?? new();

            return Ok(RespFactory.ReturnOk(monsters));
        }
    }
}
