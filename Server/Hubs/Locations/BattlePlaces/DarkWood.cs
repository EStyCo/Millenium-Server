using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Server.Hubs.Locations.BasePlaces;
using Server.Hubs.Locations.DTO;
using Server.Models.Interfaces;
using Server.Models.Monsters;

namespace Server.Hubs.Locations.BattlePlaces
{
    public class DarkWood : BattlePlace
    {
        private readonly IMapper mapper;
        private readonly IServiceFactory<UserStorage> userStorageFactory;
        public override string NamePlace { get; } = "darkwood";
        public override List<Monster> Monsters { get; protected set; } = new();
        public override Dictionary<string, ActiveUserOnPlace> ActiveUsers { get; protected set; } = new();

        public DarkWood(IMapper _mapper, 
            IHubContext<PlaceHub> hubContext,
            IServiceFactory<UserStorage> _userStorageFactory) : base(hubContext)
        {
            mapper = _mapper;
            userStorageFactory = _userStorageFactory;
        }


        public override void AddMonster()
        {
            var monster = new Orc(userStorageFactory, this);
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