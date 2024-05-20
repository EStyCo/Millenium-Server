using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Hubs.Locations;
using Server.Hubs.Locations.BasePlaces;
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
        private readonly IServiceProvider services;

        public TestController(UserStorage _userStorage, IServiceProvider _services)
        {
            userStorage = _userStorage;
            services = _services;
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

        [HttpGet("TestAreaStorage")]
        public async Task<IActionResult> TestAreaStorage(string place)
        {
            var fetchingPlace = services.GetServices<BasePlace>()
                           .FirstOrDefault(x => x.NamePlace == place);
            BasePlace result;
            
            try
            {
                result = (BattlePlace)fetchingPlace;
            }
            catch (Exception ex) 
            {
            }
                result = fetchingPlace;
            return Ok(RespFactory.ReturnOk(result));
        }

        [HttpGet("GetAllImplementsBasePlace")]
        public async Task<IActionResult> GetAllImplementsBasePlace( )
        {
            var items = services.GetServices<BasePlace>();
            List<string> result = new();

            foreach (var item in items)
            { 
                result.Add(item.Id + item.NamePlace);
            }

            return Ok(RespFactory.ReturnOk(result));
        }

        [HttpGet("GetAllImplementsBattlePlace")]
        public async Task<IActionResult> GetAllImplementsBattlePlace()
        {
            var items = services.GetServices<BasePlace>();
            List<string> result = new();

            foreach (var item in items)
            {
                var newPlace = (BattlePlace)item;
                
                result.Add(newPlace.Id + newPlace.NamePlace + newPlace.Monsters.Count);
            }

            return Ok(RespFactory.ReturnOk(result));
        }

        [HttpGet("GetGladeInBasePlace")]
        public async Task<IActionResult> TestAreaStorage()
        {
            var result = services.GetService(typeof(Glade)) as BasePlace;

            return Ok(RespFactory.ReturnOk(result.GetHashCode().ToString() + result.NamePlace));
        }
    }
}
