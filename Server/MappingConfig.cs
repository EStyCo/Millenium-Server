using AutoMapper;
using Server.Models;
using Server.Models.DTO;
using Server.Models.Monsters;

namespace Server
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Character, CharacterDTO>().ReverseMap();
            CreateMap<Monster, MonsterDTO>().ReverseMap();
        }
    }
}
