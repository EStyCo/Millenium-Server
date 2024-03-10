
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Server.Models.DTO;
using Server.Models.Monsters;

namespace Server.Models.Locations
{
    public class Glade : Area
    {
        private readonly UserStorage userStorage;
        public Glade(UserStorage _userStorage)
        {
            Monsters.Add(new Goblin());
            userStorage = _userStorage;
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
                    MonsterDTO mDTO = new();
                    mDTO.Id = m.Id;
                    mDTO.CurrentHP = m.CurrentHP;
                    mDTO.MaxHP = m.MaxHP;
                    mDTO.Name = m.Name;
                    mDTO.ImagePath = m.ImagePath;
                    monsterList.Add(mDTO);
                }

            }

            return monsterList;
        }

        public override async Task UpdateMonsters()
        {
            await Clients.All.SendAsync("UpdateList", await GetMonster());
        }

        public override async Task AttackMonster(AttackMonsterDTO attackMonster)
        {
            var monster = Monsters.FirstOrDefault(x => x.Id == attackMonster.IdMonster);
            var character = userStorage.ActiveUsers.FirstOrDefault(x => x.Character.CharacterName == attackMonster.NameCharacter);

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
