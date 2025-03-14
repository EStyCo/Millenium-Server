﻿using AutoMapper;
using Server.EntityFramework.Models;
using Server.Models.DTO.User;
using Server.Models.Entities.Monsters;
using Server.Models.Entities.Monsters.DTO;
using Server.Models.Handlers.Stats;

namespace Server.Models.Utilities
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<StatDTO, UserStatsHandler>().ReverseMap();
            CreateMap<StatsEF, UserStatsHandler>().ReverseMap();
            CreateMap<Monster, MonsterDTO>().ReverseMap();
        }
    }
}
