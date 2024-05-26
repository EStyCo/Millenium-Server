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
        public abstract void AddMonster();
        //public abstract Task HitBackMonster(Monster monster, string name);

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

        public override async Task EnterPlace(string name, int level, string connectionId)
        {
            await base.EnterPlace(name, level, connectionId);
            await UpdateMonsters();
        }

        public override async Task LeavePlace(string connectionId)
        {
            await base.LeavePlace(connectionId);
            await UpdateMonsters();
        }

        public async Task UpdateMonsters()
        {
            if (HubContext.Clients != null)
                await HubContext.Clients.Clients(ActiveUsers.Keys).SendAsync("UpdateListMonsters", Monsters);
        }

        public async Task<int> AttackMonster(AttackMonsterDTO dto, ActiveUser user)
        {
            var monster = Monsters.FirstOrDefault(x => x.Id == dto.IdMonster);
            int addingExp = 0;

            if (monster != null)
            {
                user.UseSkill(dto.Type, monster);

                if (monster.Target != user.Name) _ = monster.SetTarget(user.Name);
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
