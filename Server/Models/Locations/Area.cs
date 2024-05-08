using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Server.Models.DTO;
using Server.Models.Monsters;
using Server.Models.Skills;
using Server.Models.Utilities;

namespace Server.Models.Locations
{
    public abstract class Area : Hub
    {
        public Place CurrentArea { get; set; }
        protected List<Monster> Monsters = new();
        public abstract Task AddMonster();
        public abstract Task DeleteMonster(int id);
        public abstract Task<List<MonsterDTO>> GetMonster();
        public abstract Task UpdateMonsters();

        public override async Task OnConnectedAsync()
        {
            var connectionId = Context.ConnectionId;
            Console.WriteLine($"{connectionId} - подключился");
            await base.OnConnectedAsync();

            await Clients.Caller.SendAsync("UpdateList", Monsters);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var connectionId = Context.ConnectionId;
            Console.WriteLine($"{connectionId} - отключился");
            await base.OnDisconnectedAsync(exception);
        }


        public async Task AttackMonster(AttackMonsterDTO dto, ActiveUser character)
        {
            var monster = Monsters.FirstOrDefault(x => x.Id == dto.IdMonster);

            if (monster != null)
            {
                character.UseSkill(dto.Type, monster);

                if (monster.CurrentHP < 0)
                {
                    Monsters.Remove(monster);
                }

                await UpdateMonsters();
            }
        }
    }
}