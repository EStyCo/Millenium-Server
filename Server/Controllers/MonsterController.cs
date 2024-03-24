using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Models.DTO;
using Server.Models.Locations;
using Server.Models.Utilities;
using System.Net;

namespace Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MonsterController : ControllerBase
    {
        private readonly UserStorage userStorage;
        private readonly AreaStorage areaStorage;
        protected APIResponse response;

        public MonsterController(UserStorage _userStorage, AreaStorage _areaStorage) 
        {
            userStorage = _userStorage;
            areaStorage = _areaStorage;
            response = new();
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddMonster(PlaceDTO dto)
        {
            await areaStorage.GetArea(dto.Place).AddMonster();

            response.StatusCode = HttpStatusCode.OK;
            response.IsSuccess = true;
            return Ok(response);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteMonster(DeleteMonsterDTO dto)
        {
            await areaStorage.GetArea(dto.Place).DeleteMonster(dto.Id);

            response.StatusCode = HttpStatusCode.OK;
            response.IsSuccess = true;
            return Ok(response);
        }

        [HttpPost("attack")]
        public async Task<IActionResult> AttackMonster(AttackMonsterDTO dto)
        {
            var character = userStorage.ActiveUsers.FirstOrDefault(x => x.Character.CharacterName == dto.NameCharacter);

            await areaStorage.GetArea(dto.Place).AttackMonster(dto, character);

            response.StatusCode = HttpStatusCode.OK;
            response.IsSuccess = true;
            return Ok(response);
        }
    }
}
