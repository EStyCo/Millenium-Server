using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Models.DTO;
using Server.Models.Locations;
using System.Net;

namespace Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MonsterController : ControllerBase
    {
        private readonly UserStorage userStorage;
        private readonly Glade glade;
        protected APIResponse response;

        public MonsterController(Glade _glade, UserStorage _userStorage) 
        { 
            glade = _glade;
            userStorage = _userStorage;
            response = new();
        }

        [HttpGet("add")]
        public async Task<IActionResult> AddMonster()
        {
            await glade.AddMonster();

            response.StatusCode = HttpStatusCode.OK;
            response.IsSuccess = true;
            return Ok(response);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteMonster([FromBody] int id)
        {
            await glade.DeleteMonster(id);

            response.StatusCode = HttpStatusCode.OK;
            response.IsSuccess = true;
            return Ok(response);
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetMonster()
        {
            var monsters = await glade.GetMonster();

            response.StatusCode = HttpStatusCode.OK;
            response.IsSuccess = true;
            response.Result = monsters;
            return Ok(response);
        }

        [HttpPost("attack")]
        public async Task<IActionResult> AttackMonster(AttackMonsterDTO attackMonster)
        {
            var character = userStorage.ActiveUsers.FirstOrDefault(x => x.Character.CharacterName == attackMonster.NameCharacter);
            await glade.AttackMonster(attackMonster, character);

            response.StatusCode = HttpStatusCode.OK;
            response.IsSuccess = true;
            return Ok(response);
        }
    }
}
