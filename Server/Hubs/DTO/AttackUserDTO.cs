using Server.Models.Utilities;

namespace Server.Hubs.DTO
{
    public class AttackUserDTO
    {
        public SpellType Type { get; set; }
        public string NameUser { get; set; } = string.Empty;
        public string NameTarget { get; set; } = string.Empty;
        public string Place { get; set; } = string.Empty;
    }
}
