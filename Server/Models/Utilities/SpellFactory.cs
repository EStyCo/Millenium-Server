using Server.Models.Skills.LearningMaster;
using Server.Models.Spells;
using Server.Models.Spells.Models;

namespace Server.Models.Utilities
{
    public static class SpellFactory
    {
        private static Dictionary<SpellType, Spell> spellDictionary = new()
        {
            { SpellType.Simple, new Simple() },
            { SpellType.PowerCharge, new PowerCharge() },
            { SpellType.LowHealing, new LowHealing() },
            { SpellType.Freezing, new Freezing() },
            { SpellType.Rest, new Rest() },
            { SpellType.Bleeding, new Bleeding() },
            { SpellType.Treatment, new Treatment() },
            { SpellType.UnFreezing, new UnFreezing() },
        };

        public static Spell? Get(SpellType type)
        {
            if (spellDictionary.TryGetValue(type, out var spell))
            {
                return spell;
            }
            return null;
        }

        public static List<Spell> Get(List<SpellType> types)
        {
            var spellList = new List<Spell>();
            foreach (var type in types) 
            {
                spellList.Add(Get(type));
            }
            return spellList;
        }

        public static SpellType? Get(Spell spell)
        {
            foreach (var item in spellDictionary)
            {
                if (item.Value.GetType() == spell.GetType())
                    return item.Key;
            }
            return null;
        }

        public static List<Spell> GetAll()
        {
            return spellDictionary.Values.ToList();
        }

        public static List<MentorSpellDTO> GetMentorAllSpells()
        {
            var mentorList = new List<MentorSpellDTO>();
            foreach (var item in spellDictionary.Values)
            {
                mentorList.Add(new MentorSpellDTO()
                {
                    SpellType = item.SpellType,
                    Name = item.Name,
                    CoolDown = item.CoolDown,
                    Description = item.Description,
                    ImagePath = item.ImagePath,
                    IsLearning = false
                });
            }
            return mentorList;
        }

        public static List<MentorSpellDTO> GetLearningList(List<SpellType> listTypes)
        {
            List<MentorSpellDTO> mentorList = GetMentorAllSpells();

            foreach (var item in mentorList)
                if (listTypes.Contains(item.SpellType))
                    item.IsLearning = true;

            return mentorList;
        }
    }
}