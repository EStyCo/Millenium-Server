using Server.Hubs;
using Server.Hubs.DTO;
using Server.Repository;

namespace Server.Services
{
    public class CombatService(
        UserStorage userStorage,
        PlaceService placeService,
        UserRepository userRepository)
    {

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
