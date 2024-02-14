using AutoMapper;
using Client.MVVM.Model;
using Client.MVVM.Model.DTO;

namespace Client
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Character, CharacterDTO>().ReverseMap();
        }
    }
}
