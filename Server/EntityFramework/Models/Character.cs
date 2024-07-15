using Server.Controllers;
using Server.Models.Spells;
using Server.Models.Utilities;

namespace Server.EntityFramework.Models
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Race Race { get; set; }
        public Gender Gender { get; set; }
        public string Place { get; set; } = string.Empty;
        public int TotalSpellPoints { get; set; } = 5;
        public int FreelSpellPoints { get; set; } = 5;

        public List<SpellType> Spells { get; set; }
        public List<ItemEF> Items { get; set; }
        public Stats Stats { get; set; }
        public User User { get; set; }
    }
}
