using Server.Models.Entities;
using Server.Models.Modifiers.Unique;
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
            ImagePath = "spells/power_charge.png";
        }

        public override void Use(Entity user, params Entity[] targets)
        {
            if (user != null &&
                targets.First() != null)
            {
                var target = targets.First();
                var s = user.Stats;
                
                var defaultDmg = user.GetDefaultDamage() * 1.5 * AdditionalMultiplier();
                var dmgModifier = user.Modifiers.Get<IncreasedDamagePowerChargeModifier>();
                if (dmgModifier == null) return;
                var damage = defaultDmg + (defaultDmg / 100 * dmgModifier.Value);

                var resultDamage = target.TakeDamage((int)Math.Round(damage));
                string log = $"{user.Leading()} наполнившись невероятной силой /i{ImagePath}/i со всего размаху врезал " +
                    $"{target.Leading()} и нанёс /b{resultDamage.Count}/b /bурона./b";

                _ = StartRest();
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