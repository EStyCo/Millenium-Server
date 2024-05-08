using Server.Models.Utilities;

namespace Server.Models.DTO
{
    public class CharacterDTO
    {
        public string Name { get; set; } = string.Empty;
        public Race Race { get; set; }
        public Gender Gender { get; set; }
        public Place CurrentArea { get; set; }
        public int Level { get; set; } = 1;
        public int CurrentEXP { get; set; } = 0;
        public int ToLevelExp { get; set; } = 0;
        public int TotalSpellPoints { get; set; } = 0;
        public int FreelSpellPoints { get; set; } = 0;
        public int TotalPoints { get; set; } = 0;
        public int FreePoints { get; set; } = 5;
        public int Strength { get; set; } = 5;
        public int Agility { get; set; } = 5;
        public int Intelligence { get; set; } = 5;
        public SpellType Spell1 { get; set; } = 0;
        public SpellType Spell2 { get; set; } = 0;
        public SpellType Spell3 { get; set; } = 0;
        public SpellType Spell4 { get; set; } = 0;
        public SpellType Spell5 { get; set; } = 0;
    }
}