using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Hubs.Locations.BattlePlaces;
using Server.Models;
using Server.Models.Utilities;
using System.Diagnostics;
using System.Net;

namespace Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly UserStorage userStorage;

        public TestController(UserStorage _userStorage)
        {
            userStorage = _userStorage;
        }

        [HttpGet("ActiveUsers")]
        public async Task<IActionResult> GetActiveUsers()
        {
            await Task.Delay(10);
            if (userStorage.ActiveUsers.Count <= 0) return BadRequest(RespFactory.ReturnBadRequest("Активных юзеров нет"));

            List<string> result = new();

            foreach (var user in userStorage.ActiveUsers)
            {
                int index = userStorage.ActiveUsers.IndexOf(user);
                result.Add($"[{index}] Name: {user.Name} ConnectionId: {user.ConnectionId}");
            }
            return Ok(RespFactory.ReturnOk(result));
        }

        [HttpGet("ActiveDiscTokens")]
        public async Task<IActionResult> GetActiveDisconnectTokens()
        {
            await Task.Delay(10);
            if (userStorage.disconnectTokens.Count <= 0) return BadRequest(RespFactory.ReturnBadRequest("Активных токенов нет"));

            List<string> result = new();

            foreach (var item in userStorage.disconnectTokens)
            {
                result.Add($"ConnectionId: {item.Key} Status: {item.Value.Token.IsCancellationRequested}");
            }
            return Ok(RespFactory.ReturnOk(result));
        }

        [HttpGet]
        public async Task<IActionResult> TestServer()
        {
            //await Task.Delay(10);
            return Ok(RespFactory.ReturnOk("Server is Active"));
        }
    }
}
