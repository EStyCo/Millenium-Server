using Server.Models.Monsters.States;
using Server.Models.Utilities;

namespace Server.Models.Spells.Models
{
    public class UnFreezing : Spell
    {
        public UnFreezing()
        {
            SpellType = SpellType.UnFreezing;
            Name = "Разморозка";
            CoolDown = 5;
            Description = "Отменяет заморрозку";
            ImagePath = "freezing.png";
        }

        public override void Use(Entity user, params Entity[] targets)
        {
            if (targets.First() != null)
            {
                var target = targets.First();
                //var resultDamage = target.TakeDamage(10);
                target.RemoveState<FreezeState>();

                string log = $"{user.Name} Разморозил {target.Name}";
                SendBattleLog(log, user, target);
            }
        }
    }
}
