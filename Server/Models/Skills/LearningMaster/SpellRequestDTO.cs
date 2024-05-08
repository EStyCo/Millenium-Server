using Server.Models.Utilities;

namespace Server.Models.Skills.LearningMaster
{
    public class SpellRequestDTO
    {
        public string Name { get; set; } = string.Empty;
        public SpellType SpellType { get; set; }
    }
}
