using Server.Models.Monsters;
using Server.Models;
using Microsoft.AspNetCore.SignalR;
using Server.Hubs.DTO;
using Server.Hubs.Locations.Interfaces;
using Server.Models.Monsters.DTO;
using System.Threading;
using Server.Services;
using Server.Models.Handlers.Stats;
using Server.Repository;
using Server.Models.Interfaces;

namespace Server.Hubs.Locations.BasePlaces
{
    public abstract class BattlePlace : BasePlace, IPlaceInfo
    {
        private readonly IServiceProvider serviceProvider;

        public abstract List<Monster> Monsters { get; protected set; }
        public abstract string ImagePath { get; }
        public abstract string Description { get; }
        public virtual bool CanAttackUser { get; } = false;
        public abstract string[] Routes { get; }

        public abstract void AddMonster();


        protected BattlePlace(
            IHubContext<PlaceHub> hubContext,
            IServiceProvider _serviceProvider) : base(hubContext)
        {
            serviceProvider = _serviceProvider;
            _ = RefreshMonsters();
        }

        public void RemoveMonster(Monster _monster)
        {
            var monster = Monsters.FirstOrDefault(x => x == _monster);
            if (monster != null) Monsters.Remove(monster);

            _ = UpdateListMonsters();
        }

        public override void EnterPlace(ActiveUser user, string connectionId)
        {
            base.EnterPlace(user, connectionId);
            _ = UpdateListMonsters();
        }

        public override void LeavePlace(string connectionId)
        {
            var user = Users.FirstOrDefault(x => x.Key == connectionId);
            if (user.Value != null)
            {
                foreach (var monster in Monsters)
                    if (monster.Target == user.Value.Name)
                        _ = monster.SetTarget(string.Empty);
            }

            base.LeavePlace(connectionId);
            _ = UpdateListMonsters();
        }

        public async Task UpdateListMonsters()
        {
            if (Users.Any())
            {
                var dtoList = Monsters.Select(x => x.ToJson()).ToList();
                await HubContext.Clients.Clients(Users.Keys).SendAsync("UpdateListMonsters", dtoList);
            }
        }

        public async Task AttackMonster(AttackMonsterDTO dto, ActiveUser user)
        {
            var monster = Monsters.FirstOrDefault(x => x.Id == dto.IdMonster);
            if (monster == null) return;

            user.UseSpell(dto.Type, monster);

            if (monster.Target != user.Name) _ = monster.SetTarget(user.Name);
            if (monster.Vitality.CurrentHP <= 0) await MonsterKilled(user, monster);

            _ = UpdateListMonsters();
        }

        private async Task RefreshMonsters()
        {
            await Task.Delay(2000);
            while (true)
            {
                if (Monsters.Count < 6) AddMonster();
                await Task.Delay(new Random().Next(10, 26) * 1000);
            }
        }

        protected void ResetTargetUser(ActiveUser user)
        {
            var searchedUser = Users.FirstOrDefault(x => x.Value.Name == user.Name);

            if (searchedUser.Value != null)
            {
                _ = HubContext.Clients.Client(searchedUser.Key).SendAsync("ResetTarget");
            }
        }

        public void WeakeningPlayer(string name)
        {
            foreach (var item in Monsters)
                if (item.Target == name) item.SetTarget(string.Empty);
            _ = UpdateListMonsters();
        }

        private async Task MonsterKilled(ActiveUser user, Monster monster)
        {
            var scopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var inventoryService = scope.ServiceProvider.GetRequiredService<InventoryService>();
                var combatService = scope.ServiceProvider.GetRequiredService<CombatService>();

                combatService?.AddExp(monster.Exp, user.Name);
                (user.Stats as UserStatsHandler)?.AddExp(monster.Exp);

                var items = monster.DropItemsOnDeath().ToArray();
                if (items.Any() && inventoryService != null)
                    await inventoryService.AddItemsUser(user.Name, items);
            }

            Monsters.Remove(monster);
            ResetTargetUser(user as ActiveUser);
        }
    }
}
