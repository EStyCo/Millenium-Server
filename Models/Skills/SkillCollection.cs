using Server.Models.DTO;
using Server.Models.Skills.LearningMaster;
using Server.Models.Utilities;
using static Server.Models.ActiveUser;

namespace Server.Models.Skills
{
    public class SkillCollection
    {
        public List<Skill> CreateSkillList(List<SkillType> ActiveSkills, SendRestTimeDelegate sendRestDelegate)
        {
            List<Skill> skillList = new();

            foreach (var skill in ActiveSkills)
            {
                Skill type = PickSkill(skill);
                if (type != null)
                {
                    skillList.Add(type);
                    type.ActivateSkill(skillList.Count, sendRestDelegate);
                }
            }

            return skillList;
        }

        public List<SpellMasterDTO> CreateLearningList(List<SkillType> ActiveSkills)
        {
            List<SkillType> allSkillsType = AllSkillsType();
            List<SpellMasterDTO> skillList = new();

            foreach (var item in allSkillsType)
            {
                Skill skill = PickSkill(item);
                if (skill != null)
                {
                    SpellMasterDTO dto = new()
                    {
                        SkillType = item,
                        Name = skill.Name,
                        CoolDown = skill.CoolDown,
                        Description = skill.Description,
                        ImagePath = skill.ImagePath
                    };

                    if (ActiveSkills.Contains(item)) dto.IsLearning = true;

                    skillList.Add(dto);
                }
            }

            return skillList;
        }

        private Skill PickSkill(SkillType skill)
        {
            switch (skill)
            {
                case SkillType.Simple:
                    return new Simple();
                case SkillType.PowerCharge:
                    return new PowerCharge();
                default:
                    return null;
            }
        }

        private List<SkillType> AllSkillsType()
        {
            return new List<SkillType>()
            {
                SkillType.Simple,
                SkillType.PowerCharge
            };
        }
    }
}
