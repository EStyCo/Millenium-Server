using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Server.Hubs.Locations.BasePlaces;
using Server.Hubs.Locations.DTO;
using Server.Models.Interfaces;
using Server.Models.Monsters;

namespace Server.Hubs.Locations.BattlePlaces
{
    public class Glade : BattlePlace
    {
        private readonly IMapper mapper;
        private readonly IServiceFactory<UserStorage> userStorageFactory;
        public override string NamePlace { get; } = "glade";
        public override List<Monster> Monsters { get; protected set; } = new();
        public override Dictionary<string, ActiveUserOnPlace> ActiveUsers { get; protected set; } = new();

        public override string ImagePath { get; } = "glade.jpg";
        public override string Description { get; } = "Мирная полянка с гоблинами, будь осторожен!";
        public override string[] Routes { get; } = {"town",  "darkwood"};

        public Glade(IMapper _mapper, 
                     IHubContext<PlaceHub> hubContext,
                     IServiceFactory<UserStorage> _userStorageFactory) : base(hubContext)
        {
            mapper = _mapper;
            userStorageFactory = _userStorageFactory;
        }


        public override void AddMonster()
        {
            var monster = new Goblin(userStorageFactory, this);
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

            UpdateMonsters();
        }
    }
}
