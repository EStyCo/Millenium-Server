using Server.Controllers;
using Server.Models.Utilities;

namespace Server.Models.EntityFramework
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Race Race { get; set; }
        public Gender Gender { get; set; }
        public string Place { get; set; } = string.Empty;
        public Stats Stats { get; set; }
        public int TotalSpellPoints { get; set; } = 5;
        public int FreelSpellPoints { get; set; } = 5;
        public SpellType Spell1 { get; set; } = 0;
        public SpellType Spell2 { get; set; } = 0;
        public SpellType Spell3 { get; set; } = 0;
        public SpellType Spell4 { get; set; } = 0;
        public SpellType Spell5 { get; set; } = 0;
        public User User { get; set; }
    }
}
