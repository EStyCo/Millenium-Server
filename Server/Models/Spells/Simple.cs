using Server.Models.Utilities;

namespace Server.Models.Skills
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

        public override Tuple<int, string> Use(Entity user, params Entity[] target)
        {
            if (user is not null ||
                target.First() is not null)
            {
                var s = user.Stats;
                var damage = (s.Strength * 2) + s.Strength * (s.Agility / 100);

                var resultDamage = target.First().TakeDamage(damage);

                return new(resultDamage, target.First().Name);
            }

            return null;
        }
    }
}
