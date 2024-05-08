using Server.Models.DTO;
using Server.Models.Skills.LearningMaster;
using Server.Models.Utilities;
using static Server.Models.ActiveUser;

namespace Server.Models.Skills
{
    public class SkillCollection
    {
        public List<Spell> CreateSkillList(List<SpellType> ActiveSkilll)
        {
            List<Spell> spellList = new();

            foreach (var skill in ActiveSkilll)
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

        public List<MentorSpellDTO> CreateLearningList(List<SpellType> ActiveSkills)
        {
            List<SpellType> allSkillsType = AllSkillsType();
            List<MentorSpellDTO> skillList = new();

            foreach (var item in allSkillsType)
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

                    if (ActiveSkills.Contains(item)) dto.IsLearning = true;

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

        public SkillInfo Info(SpellType type)
        {
            switch (type)
            {
                case SpellType.Simple:
                    return new SkillInfo
                    {
                        Name = "Простой удар",
                        CoolDown = 7,
                        Description = "Обычный удар с правой, ничего выдающегося.",
                        ImagePath = "spell_simple.png"
                    };
                case SpellType.PowerCharge:
                    return new SkillInfo
                    {
                        Name = "Сильный удар",
                        CoolDown = 15,
                        Description = "Мощный удар, превосходящий обычный удар примерно в 2 раза.",
                        ImagePath = "spell_simple.png"
                    };
                default: return new SkillInfo();
            }
        }
    }
}
