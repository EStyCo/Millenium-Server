using Server.Models.DTO.User;
using Server.Models.Handlers;
using Server.Models.Modifiers;

namespace Server.Services
{
    public class StatsService(UserStorage userStorage)
    {
        public List<ModifierDTO>? GetModifiers(NameRequestDTO dto)
        {
            var user = userStorage.GetUser(dto.Name);

            return user?.Modifiers.Modifiers.Select(x => x.ToJson()).ToList();
        }
    }
}
