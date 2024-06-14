using Server.Models.Monsters;
using Server.Models.Utilities;

namespace Server.Models.Spells.Models
{
    public class PowerCharge : Spell
    {
        const int CRIT_CHANCE = 30;
        const double CRIT_MULTIPLIER = 1.4;

        public PowerCharge()
        {
            SpellType = SpellType.PowerCharge;
            Name = "Сильный удар";
            CoolDown = 15;
            Description = "За мощь, нужно платить временем.";
            ImagePath = "power_charge.png";
        }

        public override void Use(Entity user, params Entity[] targets)
        {
            if (user != null &&
                targets.First() != null)
            {
                var target = targets.First();
                var s = user.Stats;

                var damage = (s.Strength * 2 + s.Strength * (s.Agility / 100)) * 1.5 * AdditionalMultiplier();

                var resultDamage = target.TakeDamage((int)Math.Round(damage));

                string log = $"{user.Name} сильно врезал {target.Name} и нанёс {resultDamage.Count}. [{resultDamage.CurrentHP}/{resultDamage.MaxHP}]";

                SendBattleLog(log, user, target);
            }
        }

        private double AdditionalMultiplier()
        {
            if (new Random().Next(100) < CRIT_CHANCE) return CRIT_MULTIPLIER;

            return 1;
        }
    }
}
