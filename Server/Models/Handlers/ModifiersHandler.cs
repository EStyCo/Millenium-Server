using Server.Models.Modifiers.Additional.Stats;
using Server.Models.Modifiers.Additional;
using Server.Models.Modifiers.Increased;
using Server.Models.Modifiers.Default;
using Server.Models.Modifiers.Unique;
using Server.Models.Modifiers;

namespace Server.Models.Handlers
{
    public class ModifiersHandler
    {
        public List<Modifier> Modifiers { get; set; }

        public ModifiersHandler()
        {
            Modifiers = new()
            {
                new LowLimitDamage(),
                new HighLimitDamage(),

                new AdditionalHPModifier(),

                new AddStrength(),
                new AddAgility(),
                new AddVitality(),
                new AddIntelligence(),
                new AddMastery(),
                new AddLuck(),

                new AddRegeratedHP(),
                new AdditionalBleedingChance(),

                new IncreasedHPModifier(),
                new IncreasedDamageModifier(),
                new IncreasedHealingModifier(),

                new LuckyBleedingChance(),
                new IncreasedDamagePowerChargeModifier(),
            };
        }

        public void ResetValues()
        {
            Modifiers.ForEach(x => x.Reset());
        }

        public Modifier? Get<T>() where T : Modifier
        {
            return Modifiers.FirstOrDefault(x => x.GetType() == typeof(T));
        }
    }
}
