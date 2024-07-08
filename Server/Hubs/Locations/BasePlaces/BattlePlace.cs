﻿using Server.Models.Monsters;
using Server.Models;
using Microsoft.AspNetCore.SignalR;
using Server.Hubs.DTO;
using Server.Hubs.Locations.Interfaces;
using Server.Models.Monsters.DTO;

namespace Server.Hubs.Locations.BasePlaces
{
    public abstract class BattlePlace : BasePlace, IPlaceInfo
    {
        public abstract List<Monster> Monsters { get; protected set; }
        public abstract string ImagePath { get; }
        public abstract string Description { get; }
        public virtual bool CanAttackUser { get; } = false;
        public abstract string[] Routes { get; }

        public abstract void AddMonster();


        protected BattlePlace(IHubContext<PlaceHub> hubContext) : base(hubContext)
        {
            _ = RefreshMonsters();
        }

        public void RemoveMonster(Monster _monster)
        {
            var monster = Monsters.FirstOrDefault(x => x == _monster);
            if (monster != null) Monsters.Remove(monster);

            UpdateListMonsters();
        }

        public override void EnterPlace(ActiveUser user, string connectionId)
        {
            base.EnterPlace(user, connectionId);
            UpdateListMonsters();
        }

        public override void LeavePlace(string connectionId)
        {
            base.LeavePlace(connectionId);
            UpdateListMonsters();
        }

        public async void UpdateListMonsters()
        {
            if (HubContext.Clients != null)
            {
                var dtoList = new List<MonsterDTO>();
                foreach (var item in Monsters) dtoList.Add(item.ToJson());

                await HubContext.Clients.Clients(Users.Keys).SendAsync("UpdateListMonsters", dtoList);
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

                UpdateListMonsters();
            }

            return addingExp;
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
            UpdateListMonsters();
        }
    }
}
