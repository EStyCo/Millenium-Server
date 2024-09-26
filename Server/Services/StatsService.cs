using Server.Models.Modifiers;
using Server.Models.DTO.User;
using Server.Repository;
using Server.Hubs;

namespace Server.Services
{
    public class StatsService(
        UserStorage userStorage,
        UserRepository userRep)
    {

        public StatDTO? GetStats(NameRequestDTO dto) 
        {
            return userStorage.ActiveUsers
                .Where(u => u.Name == dto.Name)
                .Select(u => u.Stats)
                .FirstOrDefault()
                ?.ToJson();
        }

        public List<ModifierDTO>? GetModifiers(NameRequestDTO dto)
        {
            var user = userStorage.GetUser(dto.Name);

            return user?.Modifiers.Modifiers.Select(x => x.ToJson()).ToList();
        }

        public List<StateDTO> GetStates(NameRequestDTO dto)
        {
            return userStorage.ActiveUsers
                .Where(u => u.Name == dto.Name)
                .SelectMany(u => u.States.Keys)
                .Select(x => x.ToJson())
                .ToList();
        }

        public async Task<bool> UpdateStats(UpdateStatDTO dto)
        {
            var user = userStorage.ActiveUsers
                .Where(x => x.Name == dto.Name)
            .FirstOrDefault();

            if (user == null || !await userRep.CharacterExists(dto.Name))
                return false;

            await userRep.UpdateStats(dto);
            var newCounts = await userRep.GetStats(dto.Name);
            if (newCounts != null)
            {
                user.Stats.SetStats(newCounts);
                user.ReAssembly();
            }
            return true;
        }
    }
}
