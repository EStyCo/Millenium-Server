
using Microsoft.AspNetCore.SignalR;
using Server.Models.Monsters;

namespace Server.Models.Locations
{
    public class Glade : Area
    {
        public Glade()
        {
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
    }
}
