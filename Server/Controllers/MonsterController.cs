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
        private readonly Glade glade;
        protected APIResponse response;

        public MonsterController(Glade _glade) 
        { 
            glade = _glade;
            response = new();
        }

        [HttpGet("add")]
        public async Task<IActionResult> AddMoster()
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
            await glade.AttackMonster(attackMonster);

            response.StatusCode = HttpStatusCode.OK;
            response.IsSuccess = true;
            return Ok(response);
        }
    }
}
