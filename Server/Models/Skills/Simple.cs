using Server.Models.DTO;
using Server.Models.Monsters;
using Server.Models.Utilities;
using static Server.Models.ActiveUser;

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
            ImagePath = "spell_simple.png";
        }

        public override Task Use(Entity user, params Entity[] target)
        {
            if (user is ActiveUser and not null ||
                target[0] is Monster and not null)
            {
                var activeUser = user as ActiveUser;
                var c = activeUser?.Character;
                var damage = (c.Strength * 2) + c.Strength * (c.Agility / 100);

                var monster = target[0] as Monster;
                monster.CurrentHP -= damage;
            }

            return Task.CompletedTask;
        }
    }
}
