using Server.Hubs.DTO;
using Server.Repository;

namespace Server.Services
{
    public class CombatService
    {
        private readonly UserStorage userStorage;
        private readonly PlaceService placeService;
        private readonly UserRepository userRepository;

        public CombatService(
            UserStorage _userStorage,
            PlaceService _placeService,
            UserRepository _userRepository)
        {
            userStorage = _userStorage;
            placeService = _placeService;
            userRepository = _userRepository;
        }

        public void AttackUser() { }

        public async Task AttackMonster(AttackMonsterDTO dto)
        {
            var user = userStorage.GetUser(dto.Name);
            var place = placeService.GetBattlePlace(dto.Place);
            if (user == null || place == null) return;
            if (!user.CanAttack) return;

            await place.AttackMonster(dto, user);
        }

        public void UseSelfSpell(UseSelfSpellDTO dto) 
        {
            var user = userStorage.GetUser(dto.Name);
            var place = placeService.GetBattlePlace(dto.Place);

            if (user == null || place == null) return;
            if (!user.CanAttack) return;

            user.UseSpell(dto.Type);
            place.UpdateListUsers();
        }

        public void AddExp(int exp, string name)
        {
            var user = userStorage.GetUser(name);
            if(user != null)
            {
                _ = userRepository.UpdateExp(exp, name);
            }
        }
    }
}
