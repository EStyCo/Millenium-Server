using AutoMapper;
using Client.MVVM.Model;
using Client.MVVM.Model.DTO;
using Client.MVVM.Model.DTO.Auth;

namespace Client
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Character, CharacterDTO>().ReverseMap();
            CreateMap<Monster, MonsterDTO>().ReverseMap();
            CreateMap<RegData, RegRequestDTO>().ReverseMap();
        }
    }
}
