using Server.Models.Monsters;
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

        public override Task Use(Entity user, params Entity[] target)
        {
            if (user is ActiveUser and not null ||
                target[0] is Monster and not null)
            {
                var activeUser = user as ActiveUser;
                var s = activeUser?.Stats;
                var damage = (s.Strength * 2) + s.Strength * (s.Agility / 100);

                var monster = target[0] as Monster;
                monster.CurrentHP -= damage;
            }

            return Task.CompletedTask;
        }
    }
}
