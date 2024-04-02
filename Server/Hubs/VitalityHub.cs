using Microsoft.AspNetCore.SignalR;
using Server.Models;
using System.Security.Claims;

namespace Server.Hubs
{
    public class VitalityHub : Hub
    {
        /*private bool isRunning = false;
        //private readonly ActiveUser user;
        private readonly UserStorage userStorage;

        public VitalityHub(UserStorage _userStorage)
        {
            userStorage = _userStorage;
        }

        public async Task ConnectHub(string playerName)
        {
            var user = userStorage.ActiveUsers.FirstOrDefault(x => x.Character.CharacterName == playerName);
            user.ConnectionId = Context.ConnectionId;

            if (!isRunning)
            { 
                isRunning = true;
                await StartHub();
            }
        }

        public async Task StartHub()
        {
            while (true)
            {
                foreach (var user in userStorage.ActiveUsers)
                {
                    await SendHP(user);
                    await SendMP(user);
                }
                await Task.Delay(1000);
            }
        }

        public async Task SendHP(ActiveUser user)
        {
            if (user.CurrentHP < user.MaxHP)
            {
                user.CurrentHP += user.RegenRateHP;
            }

            if (Clients != null)
            {
                await Clients.Client(user.ConnectionId).SendAsync("UpdateHP", user.CurrentHP);
            }
        }

        public async Task SendMP(ActiveUser user)
        {
            if (user.CurrentMP < user.MaxMP)
            {
                user.CurrentMP += user.RegenRateMP;
            }

            if (Clients != null)
            {
                await Clients.Client(user.ConnectionId).SendAsync("UpdateMP", user.CurrentMP);

            }
        }*/
    }
}
