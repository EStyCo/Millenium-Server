using Server.Models.Utilities;

namespace Server.Models.DTO
{
    public class AttackMonsterDTO
    {
        public int IdMonster { get; set; }
        public SpellType Type { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Place { get; set; } = string.Empty;
    }
}
