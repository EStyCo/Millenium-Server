using Server.Models.Spells.States;
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
            Description = "Отменяет заморозку";
            ImagePath = "spells/freezing.png";
        }

        public override void Use(Entity user, params Entity[] targets)
        {
            if (targets.First() != null)
            {
                var target = targets.First();
                target.RemoveState<FreezeState>();

                _ = StartRest();
                string log = $"{user.Leading()} согрел своей любовью и разморозил {target.Leading()}";
                SendBattleLog(log, user, target);
            }
        }
    }
}
