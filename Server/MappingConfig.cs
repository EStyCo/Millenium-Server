﻿using AutoMapper;
using Server.Models.DTO;
using Server.Models.EntityFramework;
using Server.Models.Handlers.Stats;
using Server.Models.Monsters;
using Server.Models.Monsters.DTO;

namespace Server
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Character, CharacterDTO>().ReverseMap();
            CreateMap<StatDTO, UserStatsHandler>().ReverseMap();
            CreateMap<Stats, UserStatsHandler>().ReverseMap();
            CreateMap<UpdateStatDTO, CharacterDTO>();
            CreateMap<UpdateStatDTO, Character>();
            CreateMap<Monster, MonsterDTO>().ReverseMap();
        }
    }
}
