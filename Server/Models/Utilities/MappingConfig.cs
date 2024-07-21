using AutoMapper;
using Server.EntityFramework.Models;
using Server.Models.DTO.User;
using Server.Models.Handlers.Stats;
using Server.Models.Monsters;
using Server.Models.Monsters.DTO;

namespace Server.Models.Utilities
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<CharacterEF, CharacterDTO>().ReverseMap();
            CreateMap<StatDTO, UserStatsHandler>().ReverseMap();
            CreateMap<Stats, UserStatsHandler>().ReverseMap();
            CreateMap<UpdateStatDTO, CharacterDTO>();
            CreateMap<UpdateStatDTO, CharacterEF>();
            CreateMap<Monster, MonsterDTO>().ReverseMap();
        }
    }
}
