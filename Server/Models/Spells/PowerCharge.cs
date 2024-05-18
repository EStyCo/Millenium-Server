using Server.Models.DTO;
using Server.Models.Monsters;
using Server.Models.Utilities;
using static Server.Models.ActiveUser;

namespace Server.Models.Skills
{
    public class PowerCharge : Spell
    {
        public PowerCharge()
        {
            SpellType = SpellType.PowerCharge;
            Name = "Сильный удар";
            CoolDown = 15;
            Description = "За мощь, нужно платить временем.";
            ImagePath = "power_charge.png";
        }

        public override Task Use(Entity user, params Entity[] target)
        {
            if (user is ActiveUser and not null ||
                target[0] is Monster and not null)
            {
                var activeUser = user as ActiveUser;
                var s = activeUser?.Stats;
                var damage = ((s.Strength * 2) + s.Strength * (s.Agility / 100)) * 1.6;

                var monster = target[0] as Monster;
                monster.CurrentHP -= (int)Math.Round(damage);
            }
            return Task.CompletedTask;
        }
    }
}
