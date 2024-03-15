using Server.Models.Utilities;

namespace Server.Models.DTO
{
    public class CharacterDTO
    {
        public string CharacterName { get; set; } = string.Empty;
        public Race Race { get; set; }
        public Gender Gender { get; set; }
        public Place CurrentArea { get; set; }
        public int Level { get; set; } = 1;
        public int Exp { get; set; } = 0;
        public int TotalSpellPoints { get; set; } = 0;
        public int FreelSpellPoints { get; set; } = 0;
        public int TotalPoints { get; set; } = 0;
        public int FreePoints { get; set; } = 5;
        public int Strength { get; set; } = 5;
        public int Agility { get; set; } = 5;
        public int Intelligence { get; set; } = 5;
        public SkillType Skill1 { get; set; } = 0;
        public SkillType Skill2 { get; set; } = 0;
        public SkillType Skill3 { get; set; } = 0;
        public SkillType Skill4 { get; set; } = 0;
        public SkillType Skill5 { get; set; } = 0;
    }
}