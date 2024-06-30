using Server.Models.DTO;

namespace Server.Hubs.DTO
{
    public class UserOnPlace(string name, int level, int currentHP, int maxHP, List<StateDTO> states)
    {
        public string? Name { get; } = name;
        public int? Level { get; } = level;
        public int? CurrentHP { get; } = currentHP;
        public int? MaxHP { get; } = maxHP;
        public List<StateDTO>? States { get; } = states;
    }
}