using Server.Models.Spells.States;
using Server.Models.Utilities;

namespace Server.Models.Spells.Models
{
    public class Freezing : Spell
    {
        public Freezing()
        {
            SpellType = SpellType.Freezing;
            Name = "Заморозка";
            CoolDown = 35;
            Description = "Замораживает противника на некоторое время";
            ImagePath = "freezing.png";
        }

        public override void Use(Entity user, params Entity[] targets)
        {
            if (targets.First() != null)
            {
                var target = targets.First();
                var resultDamage = target.TakeDamage(10);
                target.AddState<FreezeState>(user);

                string log = $"{user.Name} наслал снежную метель на {target.Name} и нанёс {resultDamage.Count}. [{resultDamage.CurrentHP}/{resultDamage.MaxHP}]\n{target.Name} Заморожен!";

                _ = StartRest();
                SendBattleLog(log, user, target);
            }
        }
    }
}
