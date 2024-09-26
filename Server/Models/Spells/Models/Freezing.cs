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
            ImagePath = "spells/freezing.png";
        }

        public override void Use(Entity user, params Entity[] targets)
        {
            if (targets.First() != null)
            {
                var target = targets.First();
                var resultDamage = target.TakeDamage(10);
                target.AddState<FreezeState>(user);

                string log = $"{user.Leading()} наслал снежную метель /i{ImagePath}/i на {target.Leading()} .Нанесено /b{resultDamage.Count}/b /bурона./b";

                _ = StartRest();
                SendBattleLog(log, user, target);

                log = $"/i{target.ImagePath}/i /b{target.Name}/b Заморожен! /i{ImagePath}/i";
                SendBattleLog(log, user, target);
            }
        }
    }
}
