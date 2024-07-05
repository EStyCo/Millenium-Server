using Server.Models.Utilities;

namespace Server.Models.Spells.Models
{
    public class Simple : Spell
    {
        public Simple()
        {
            SpellType = SpellType.Simple;
            Name = "Простой удар";
            CoolDown = 7;
            Description = "Обычный удар с правой, ничего выдающегося.";
            ImagePath = "simple.png";
        }

        public override void Use(Entity user, params Entity[] targets)
        {
            if (user != null &&
                targets.First() != null)
            {
                var target = targets.First();
                var s = user.Stats;
                var damage = s.Strength * 2 + s.Strength * (s.Agility / 100);

                var resultDamage = target.TakeDamage(damage);

                string log = $"{user.Name} ударил {target.Name} и нанёс {resultDamage.Count}. [{resultDamage.CurrentHP}/{resultDamage.MaxHP}]";

                _ = StartRest();
                SendBattleLog(log, user, targets.First());
            }
        }
    }
}
