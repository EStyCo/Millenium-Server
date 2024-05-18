using Server.Models.Skills.LearningMaster;
using Server.Models.Utilities;

namespace Server.Models.Skills
{
    public class SkillCollection
    {
        public List<Spell> CreateSkillList(List<SpellType> ActiveSpell)
        {
            List<Spell> spellList = new();

            foreach (var skill in ActiveSpell)
            {
                Spell type = PickSkill(skill);
                if (type != null)
                {
                    spellList.Add(type);
                    type.SpellType = skill;
                }
            }

            return spellList;
        }

        public List<MentorSpellDTO> CreateLearningList(List<SpellType> ActiveSpell)
        {
            List<SpellType> allSpellsType = AllSkillsType();
            List<MentorSpellDTO> skillList = new();

            foreach (var item in allSpellsType)
            {
                Spell skill = PickSkill(item);
                if (skill != null)
                {
                    MentorSpellDTO dto = new()
                    {
                        SpellType = skill.SpellType,
                        Name = skill.Name,
                        CoolDown = skill.CoolDown,
                        Description = skill.Description,
                        ImagePath = skill.ImagePath
                    };

                    if (ActiveSpell.Contains(item)) dto.IsLearning = true;

                    skillList.Add(dto);
                }
            }

            return skillList;
        }

        public Spell PickSkill(SpellType skill)
        {
            switch (skill)
            {
                case SpellType.Simple:
                    return new Simple();
                case SpellType.PowerCharge:
                    return new PowerCharge();
                default:
                    return null;
            }
        }

        public Type GetSkillType(SpellType skill)
        {
            switch (skill)
            {
                case SpellType.Simple:
                    return typeof(Simple);
                case SpellType.PowerCharge:
                    return typeof(PowerCharge);
                default:
                    return null;
            }
        }

        private List<SpellType> AllSkillsType()
        {
            return new List<SpellType>()
            {
                SpellType.Simple,
                SpellType.PowerCharge
            };
        }
    }
}
