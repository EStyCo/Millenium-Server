using Server.Models.Skills.LearningMaster;

namespace Server.Models.DTO.User
{
    public class MentorSpellListResponseDTO
    {
        public int FreePoints { get; set; }
        public int TotalPoints { get; set; }
        public List<MentorSpellDTO> SpellList { get; set; } = new();
    }
}