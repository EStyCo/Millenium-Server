
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

        public override async Task<List<Monster>> GetMonster()
        {
            await Task.Delay(10);

            if (Monsters.Count > 0)
            { 
                return Monsters;
            }

            return null;
        }

        public override async Task UpdateMonsters()
        {
            await Clients.All.SendAsync("UpdateList", Monsters);
        }

        public override async Task AttackMonster(AttackMonsterDTO attackMonster)
        { 
            var monster = Monsters.FirstOrDefault(x => x.Id == attackMonster.IdMonster);
            var character = userStorage.ActiveUsers.FirstOrDefault(x => x.Character.CharacterName == attackMonster.NameCharacter);

            if (monster != null && character != null)
            {
                var skill = character.ActiveSkills.FirstOrDefault(x => x.Name == attackMonster.SkillNaming);
                var damage = skill.Attack(character.Character);
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
