using Server.Models.DTO;
using Server.Models.Monsters;
using Server.Models;
using Microsoft.AspNetCore.SignalR;

namespace Server.Hubs.Locations.BasePlaces
{
    public abstract class BattlePlace : BasePlace
    {
        protected BattlePlace(IHubContext<PlaceHub> hubContext) : base(hubContext)
        {
        }

        public abstract List<Monster> Monsters { get; protected set; }
        public abstract Task AddMonster();
        //public abstract Task UpdateMonsters();
        //public abstract Task<int> AttackMonster(AttackMonsterDTO dto, ActiveUser user);


        public async Task DeleteMonster(int id)
        {
            await Task.Delay(10);

            var monster = Monsters.FirstOrDefault(x => x.Id == id);

            if (monster != null)
            {
                Monsters.Remove(monster);
            }

            await UpdateMonsters();
        }

        /*public override async Task OnConnectedAsync()
        {
            var connectionId = Context.ConnectionId;
            Console.WriteLine($"{connectionId} - подключился к {NamePlace}");
            await base.OnConnectedAsync();

            await Clients.Caller.SendAsync("UpdateListMonsters", Monsters);
            await Clients.Caller.SendAsync("UpdateListUsers", ActiveUsers);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var connectionId = Context.ConnectionId;
            Console.WriteLine($"{connectionId} - отключился от {NamePlace} {this.GetHashCode().ToString()}");
            await base.OnDisconnectedAsync(exception);
        }*/

        public async Task UpdateMonsters()
        {
            if (HubContext.Clients != null)
                await HubContext.Clients.All.SendAsync("UpdateListMonsters", Monsters);
        }

        public async Task<int> AttackMonster(AttackMonsterDTO dto, ActiveUser user)
        {
            Console.WriteLine($"{NamePlace} {this.GetHashCode().ToString()}");
            var monster = Monsters.FirstOrDefault(x => x.Id == dto.IdMonster);
            int addingExp = 0;

            if (monster != null)
            {
                user.UseSkill(dto.Type, monster);

                if (monster.CurrentHP < 0)
                {
                    Monsters.Remove(monster);
                    addingExp = monster.Exp;
                }

                await UpdateMonsters();
            }
            return addingExp;
        }
    }
}
