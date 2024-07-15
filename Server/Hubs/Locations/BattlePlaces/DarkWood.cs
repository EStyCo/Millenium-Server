﻿using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Server.Hubs.DTO;
using Server.Hubs.Locations.BasePlaces;
using Server.Models;
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
        public override Dictionary<string, ActiveUser> Users { get; protected set; } = new();

        public override string ImagePath { get; } = "locations/darkwood.jpg";
        public override string Description { get; } = "поле для драки, ебаштесь";
        public override string[] Routes { get; } = { "glade", "masturbation" };

        public DarkWood(IMapper _mapper,
                        IHubContext<PlaceHub> hubContext,
                        IServiceFactory<UserStorage> _userStorageFactory) : base(hubContext)
        {
            mapper = _mapper;
            userStorageFactory = _userStorageFactory;
        }


        public override void AddMonster()
        {
            var action = UpdateListMonsters;
            var monster = new Orc(userStorageFactory, this, action);
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