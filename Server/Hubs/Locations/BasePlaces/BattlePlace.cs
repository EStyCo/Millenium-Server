using Server.Models.DTO;
using Server.Models.Monsters;
using Server.Models;
using Microsoft.AspNetCore.SignalR;

namespace Server.Hubs.Locations.BasePlaces
{
    public abstract class BattlePlace : BasePlace
    {
        public abstract string ImagePath { get; }
        public abstract string Description { get; }
        public abstract List<Monster> Monsters { get; protected set; }
        public abstract void AddMonster();
        public abstract string[] Routes { get; }

        protected BattlePlace(IHubContext<PlaceHub> hubContext) : base(hubContext)
        {
            _ = RefreshMonsters();
        }

        public void RemoveMonster(Monster _monster)
        {
            var monster = Monsters.FirstOrDefault(x => x == _monster);
            if (monster != null) Monsters.Remove(monster);

            UpdateMonsters();
        }

        public override async Task EnterPlace(string name, int level, string connectionId)
        {
            await base.EnterPlace(name, level, connectionId);
            //UpdateDescription();
            UpdateMonsters();
        }

        public override async Task LeavePlace(string connectionId)
        {
            await base.LeavePlace(connectionId);
            UpdateMonsters();
        }

        public async void UpdateMonsters()
        {
            if (HubContext.Clients != null)
            {
                var dtoList = new List<MonsterDTO>();
                foreach (var item in Monsters) dtoList.Add(item.ToJson());

                await HubContext.Clients.Clients(ActiveUsers.Keys).SendAsync("UpdateListMonsters", dtoList);
            }
        }

        public async void UpdateDescription()
        {
            if (HubContext.Clients != null)
            {
                await HubContext.Clients.Clients(ActiveUsers.Keys).SendAsync("UpdateDescription", new string[]{ ImagePath, Description });
            }
        }

        public int AttackMonster(AttackMonsterDTO dto, ActiveUser user)
        {
            var monster = Monsters.FirstOrDefault(x => x.Id == dto.IdMonster);
            int addingExp = 0;

            if (monster != null)
            {
                user.UseSpell(dto.Type, monster);

                if (monster.Target != user.Name) monster.SetTarget(user.Name);
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
            await Task.Delay(2000);

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

        public void WeakeningPlayer(string name)
        {
            foreach (var item in Monsters)
            {
                if (item.Target == name) item.SetTarget(string.Empty);
            }

            UpdateMonsters();
        }
    }
}
