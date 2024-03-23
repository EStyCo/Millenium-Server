using Microsoft.AspNetCore.SignalR;
using Server.Models.DTO;
using Server.Models.Monsters;

namespace Server.Models.Locations
{
    public class Glade : Area
    {
        public Glade()
        {
            Monsters.Add(new Goblin());
            Monsters.Add(new Goblin());
        }

        public override async Task AddMonster()
        {
            await Task.Delay(10);

            var monster = new Goblin();
            if (Monsters.Count == 0)
            {
                monster.Id = 0;
            }
            else
            {
                int maxId = Monsters.Max(x => x.Id);
                monster.Id = maxId + 1;
            }
            Monsters.Add(monster);

            await UpdateMonsters();
        }

        public override async Task DeleteMonster(int id)
        {
            await Task.Delay(10);

            var monster = Monsters.FirstOrDefault(x => x.Id == id);

            if (monster != null)
            {
                Monsters.Remove(monster);
            }

            await UpdateMonsters();
        }

        public override async Task<List<MonsterDTO>> GetMonster()
        {
            await Task.Delay(10);
            List<MonsterDTO> monsterList = new();

            if (Monsters.Count > 0)
            {
                foreach (var m in Monsters)
                {
                    monsterList.Add(new MonsterDTO
                    {
                        Id = m.Id,
                        CurrentHP = m.CurrentHP,
                        MaxHP = m.MaxHP,
                        Name = m.Name,
                        ImagePath = m.ImagePath
                    });
                }
            }

            return monsterList;
        }

        public override async Task UpdateMonsters()
        {
            await Clients.All.SendAsync("UpdateList", await GetMonster());
        }

        public override async Task AttackMonster(AttackMonsterDTO attackMonster, ActiveUser character)
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
