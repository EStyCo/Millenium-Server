using Microsoft.AspNetCore.Mvc;
using Server.Hubs;
using Server.Models;
using Server.Models.DTO.Auth;
using Server.Models.DTO.User;
using Server.Models.Utilities;
using Server.Repository;
using System.Net;

namespace WebApplication1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LogsController(
        UserStorage userStorage) : ControllerBase
    {
        [HttpPost("get")]
        public IActionResult GetBattleLogs(NameRequestDTO dto)
        {
            var user = userStorage.GetUser(dto.Name);
            if (user != null)
                return Ok(RespFactory.ReturnOk(new CustomList<BattleLog>(user.BattleLogs)));
            return BadRequest(RespFactory.ReturnBadRequest(new CustomList<string>(new List<string>())));
        }
    }
}

