using Server.Models.DTO.Auth;
using Server.Models.DTO.User;
using Server.Repository;

namespace Server.Services
{
    public class AuthService
    {
        private readonly UserStorage userStorage;
        private readonly UserRepository userRep;
        private readonly UserFactory userFactory;

        public AuthService(UserStorage _userStorage,
                           UserRepository _userRepository,
                           UserFactory _userFactory)
        {
            userStorage = _userStorage;
            userRep = _userRepository;
            userFactory = _userFactory;
        }

        public async Task<CharacterDTO?> LoginUser(LoginRequestDTO dto)
        {
            var character = await userRep.LoginUser(dto);
            if (character == null) return null;

            userFactory.LoginUser(character);
            return new()
            {
                Name = character.Name,
                Place = character.Place,
            };
        }

        public async Task<bool> RegistrationNewUser(RegRequestDTO dto)
        {
            if (!await userRep.IsUniqueUser(dto) || !await userRep.Registration(dto))
            {
                return false;
            }
            return true;
        }
    }

    /*private async Task<bool> CreateActiveUser(string name)
    {
        var character = await userRep.GetCharacter(name);
        var stats = await userRep.GetStats(name);
        var items = await userRep.GetInventory(name);

        if (character == null || stats == null || items == null)
            return false;

        userStorage.LoginUser(stats, character, items);
        return true;
    }*/
}

