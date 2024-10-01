using Server.Hubs;
using Server.Hubs.DTO;
using Server.Models.Entities;
using Server.Models.Entities.Monsters;
using Server.Models.Handlers.Stats;
using Server.Models.Utilities;
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

        public async void AddExp(int exp, string name)
        {
            var user = userStorage.GetUser(name);
            if (user == null) return;
            var stats = user.Stats as UserStatsHandler;
            if(stats == null) return;

            stats.CurrentExp += exp;
            if (stats.CurrentExp >= stats.ToLevelExp)
            {
                if(!await userRepository.LevelUp(user.Name)) return;
                var newLvlPair = new LevelFactory().LevelUp(stats.Level);
                stats.Level = newLvlPair.Key;
                stats.ToLevelExp = newLvlPair.Value;
                stats.FreePoints += 5;
                _ = user.AddBattleLog($"Вы достигли {stats.Level} уровня. Мои поздравления!");
            }


            //(user.Stats as UserStatsHandler)?.UpdateExp(exp);
            _ = userRepository.UpdateExp(exp, name);

        }
    }
}
