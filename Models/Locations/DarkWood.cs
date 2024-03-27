using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Server.Models.DTO;
using Server.Models.Monsters;

namespace Server.Models.Locations
{
    public class DarkWood : Area
    {
        private readonly IMapper mapper;

        public DarkWood(IMapper _mapper) 
        {
            mapper = _mapper;

            Monsters.Add(new Orc() { Id = 0 });
            Monsters.Add(new Orc() { Id = 1 });
            Monsters.Add(new Orc() { Id = 2 });
        }

        public override async Task AddMonster()
        {
            var monster = new Orc();
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

        public override Task DeleteMonster(int id)
        {
            throw new NotImplementedException();
        }

        public async override Task<List<MonsterDTO>> GetMonster()
        {
            await Task.Delay(10);
            List<MonsterDTO> monsterList = new();

            if (Monsters.Count > 0)
            {
                foreach (var m in Monsters)
                {
                    monsterList.Add(mapper.Map<MonsterDTO>(m));
                }
            }

            return monsterList;
        }

        public async override Task UpdateMonsters()
        {
            await Clients.All.SendAsync("UpdateList", await GetMonster());
        }
    }
}
