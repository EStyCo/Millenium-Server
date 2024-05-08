using Server.Models.Skills;
using Server.Models.Skills.LearningMaster;

namespace Server.Models.DTO
{
    public class MentorSpellListResponseDTO
    {
        public int FreePoints { get; set; }
        public List<MentorSpellDTO> SpellList { get; set; } = new();
    }
}