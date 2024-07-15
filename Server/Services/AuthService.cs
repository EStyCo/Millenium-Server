using Server.Models.DTO.Auth;
using Server.Repository;

namespace Server.Services
{
    public class AuthService
    {
        private readonly UserStorage userStorage;
        private readonly UserRepository userRep;
        private readonly InventoryService invService;

        public AuthService(UserStorage _userStorage,
                           UserRepository _userRepository,
                           InventoryService _invService)
        {
            userStorage = _userStorage;
            userRep = _userRepository;
            invService = _invService;
        }

        public async Task<LoginResponseDTO?> LoginUser(LoginRequestDTO dto)
        {
            var responseUser = await userRep.LoginUser(dto);
            if (responseUser == null) return null;

            if(await CreateActiveUser(responseUser.Character.Name))
            {
                return responseUser;
            }
            return null;
        }

        public async Task<bool> RegistrationNewUser(RegRequestDTO dto)
        {
            if (!await userRep.IsUniqueUser(dto) || !await userRep.Registration(dto))
            {
                return false;
            }
            return true;
        }

        private async Task<bool> CreateActiveUser(string name)
        {
            var character = await userRep.GetCharacter(name);
            var stats = await userRep.GetStats(name);
            var items = await userRep.GetInventory(name);

            if (character == null || stats == null || items == null)
                return false;

            userStorage.LoginUser(stats, character, items);
            return true;
        }
    }
}
