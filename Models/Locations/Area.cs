using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Server.Models.DTO;
using Server.Models.Monsters;
using Server.Models.Utilities;

namespace Server.Models.Locations
{
    public abstract class Area : Hub
    {
        public Place CurrentArea {  get; set; }
        protected List<Monster> Monsters = new();
        public abstract Task AddMonster();
        public abstract Task DeleteMonster(int id);
        public abstract Task<List<MonsterDTO>> GetMonster();
        public abstract Task UpdateMonsters();

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();

            await Clients.Caller.SendAsync("UpdateList", Monsters);
        }

        public async Task AttackMonster(AttackMonsterDTO attackMonster, ActiveUser character)
        {
            var monster = Monsters.FirstOrDefault(x => x.Id == attackMonster.IdMonster);

            if (monster != null && character != null)
            {
                var skill = character.ActiveSkills.FirstOrDefault(x => x.Id == attackMonster.SkillId);
                var damage = await skill.Attack(character.Character);
                var hp = monster.CurrentHP -= damage;

                if (hp <= 0)
                {
                    Monsters.Remove(monster);
                }

                await UpdateMonsters();
            }
        }
    }
}