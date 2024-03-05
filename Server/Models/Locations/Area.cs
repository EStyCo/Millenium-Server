using Microsoft.AspNetCore.SignalR;
using Server.Models.Monsters;
using Server.Models.Utilities;
using System.Diagnostics;

namespace Server.Models.Locations
{
    public abstract class Area : Hub
    {
        public Place CurrentArea {  get; set; }
        protected List<Monster> Monsters = new();
        public abstract Task AddMonster();
        public abstract Task DeleteMonster(int id);
        public abstract Task<List<Monster>> GetMonster();
        public abstract Task UpdateMonsters();

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();

            await Clients.Caller.SendAsync("UpdateList", Monsters);
        }
    }
}