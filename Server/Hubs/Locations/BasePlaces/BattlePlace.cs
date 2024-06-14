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
            _ = RefreshMonsters();
        }

        public abstract List<Monster> Monsters { get; protected set; }
        public abstract void AddMonster();

        public void RemoveMonster(Monster _monster)
        {
            var monster = Monsters.FirstOrDefault(x => x == _monster);
            if (monster != null) Monsters.Remove(monster);

            UpdateMonsters();
        }

        public override async Task EnterPlace(string name, int level, string connectionId)
        {
            await base.EnterPlace(name, level, connectionId);
            UpdateMonsters();
        }

        public override async Task LeavePlace(string connectionId)
        {
            await base.LeavePlace(connectionId);
            UpdateMonsters();
        }

        public async void UpdateMonsters()
        {
            /*List<Monster> removeList = new();
            foreach (var monster in Monsters)
            {
                if (monster.Vitality.CurrentHP <= 0)
                { 
                    removeList.Add(monster);
                }
            }

            foreach (var monster in removeList) 
            {
                Monsters.Remove(monster);
            }*/

            if (HubContext.Clients != null)
            {
                var dtoList = new List<MonsterDTO>();
                foreach (var item in Monsters) dtoList.Add(item.ToJson());

                await HubContext.Clients.Clients(ActiveUsers.Keys).SendAsync("UpdateListMonsters", dtoList);
            }
        }

        public int AttackMonster(AttackMonsterDTO dto, ActiveUser user)
        {
            var monster = Monsters.FirstOrDefault(x => x.Id == dto.IdMonster);
            int addingExp = 0;

            if (monster != null)
            {
                user.UseSpell(dto.Type, monster);

                if (monster.Target != user.Name) _ = monster.SetTarget(user.Name);
                if (monster.Vitality.CurrentHP <= 0)
                {
                    Monsters.Remove(monster);
                    addingExp = monster.Exp;
                    ResetTargetUser(user);
                }

                UpdateMonsters();
            }

            return addingExp;
        }

        private async Task RefreshMonsters()
        {
            await Task.Delay(10000);

            while (true)
            {
                if (Monsters.Count < 6)
                {
                    AddMonster();
                }

                await Task.Delay(new Random().Next(10, 26) * 1000);
            }
        }

        protected void ResetTargetUser(ActiveUser user)
        {
            var searchedUser = ActiveUsers.FirstOrDefault(x => x.Value.Name == user.Name);

            if (searchedUser.Value != null)
            {
                _ = HubContext.Clients.Client(searchedUser.Key).SendAsync("ResetTarget");
            }
        }
    }
}
