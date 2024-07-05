using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Server.Hubs.Locations.BasePlaces;
using Server.Models.Interfaces;
using Server.Models.Monsters;
using Server.Models;
using System;

namespace Server.Hubs.Locations.BattlePlaces
{
    public class PizzaLand : BattlePlace
    {
        private readonly IMapper mapper;
        private readonly IServiceFactory<UserStorage> userStorageFactory;
        public override string NamePlace { get; } = "pizzaland";
        public override List<Monster> Monsters { get; protected set; } = new();
        public override Dictionary<string, ActiveUser> Users { get; protected set; } = new();

        public override string ImagePath { get; } = "pizzaland.png";
        public override string Description { get; } = "Загадочная пиццерия, можно вкусно покушать, если выживешь.";
        public override string[] Routes { get; } = { "glade" };

        public PizzaLand(IMapper _mapper,
                     IHubContext<PlaceHub> hubContext,
                     IServiceFactory<UserStorage> _userStorageFactory) : base(hubContext)
        {
            mapper = _mapper;
            userStorageFactory = _userStorageFactory;
        }


        public override void AddMonster()
        {
            var action = UpdateListMonsters;
            Monster monster = new PizzaPiece(userStorageFactory, this, action);

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

            UpdateListMonsters();
        }
    }
}
