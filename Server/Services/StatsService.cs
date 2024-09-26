using Microsoft.AspNetCore.Mvc;
using Server.Hubs;
using Server.Models.DTO.User;
using Server.Models.Handlers;
using Server.Models.Handlers.Stats;
using Server.Models.Modifiers;
using Server.Models.Utilities;

namespace Server.Services
{
    public class StatsService(UserStorage userStorage)
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
    }
}
