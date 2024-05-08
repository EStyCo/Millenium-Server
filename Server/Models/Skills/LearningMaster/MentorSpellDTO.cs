using Server.Models.Utilities;

namespace Server.Models.Skills.LearningMaster
{
    public class MentorSpellDTO
    {
        public SpellType SpellType { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CoolDown { get; set; }
        public string Description { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public bool IsLearning { get; set; } = false;
    }
}