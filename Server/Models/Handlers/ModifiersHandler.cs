using Server.Models.Modifiers;
using Server.Models.Modifiers.Additional;
using Server.Models.Modifiers.Increased;

namespace Server.Models.Handlers
{
    public class ModifiersHandler
    {
        public List<Modifier> Modifiers { get; set; }

        public ModifiersHandler()
        {
            Modifiers = new()
            {
                new AdditionalHPModifier(),
                new IncreasedHPModifier(),
                new IncreasedDamageModifier(),
                new IncreasedHealingModifier()
            };
        }

        public void ResetValues()
        {
            foreach (var modifier in Modifiers)
            {
                modifier.Reset();
            }
        }

        public Modifier? Get<T>() where T : Modifier
        {
            return Modifiers.FirstOrDefault(x => x.GetType() == typeof(T));
        }
    }
}
