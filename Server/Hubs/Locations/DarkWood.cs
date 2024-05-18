using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Server.Hubs.Locations.BasePlaces;
using Server.Hubs.Locations.DTO;
using Server.Models.Monsters;

namespace Server.Hubs.Locations
{
    public class DarkWood : BattlePlace
    {
        private readonly IMapper mapper;
        public override string NamePlace { get; } = "darkwood";
        public override List<Monster> Monsters { get; protected set; } = new();
        public override Dictionary<string, ActiveUserOnPlace> ActiveUsers { get; protected set; } = new();

        public DarkWood(IMapper _mapper, IHubContext<PlaceHub> hubContext) : base(hubContext)
        {
            mapper = _mapper;

            Monsters.Add(new Goblin() { Id = 0 });
            Monsters.Add(new Goblin() { Id = 1 });
            Monsters.Add(new Goblin() { Id = 2 });
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
    }
}
