using Server.Models.Utilities;

namespace Server.Hubs.DTO
{
    public class UseSelfSpellDTO
    {
        public SpellType Type { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Place { get; set; } = string.Empty;
    }
}
