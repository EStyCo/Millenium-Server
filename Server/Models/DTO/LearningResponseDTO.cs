using Server.Models.Skills;
using Server.Models.Skills.LearningMaster;

namespace Server.Models.DTO
{
    public class LearningResponseDTO
    {
        public int FreePoints { get; set; }
        public List<SpellMasterDTO> AllSkills { get; set; }
    }
}
