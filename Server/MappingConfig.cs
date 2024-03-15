using AutoMapper;
using Server.Models;
using Server.Models.DTO;

namespace Server
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Character, CharacterDTO>().ReverseMap();
        }
    }
}
