using Server.Models.Modifiers.Increased;
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
            ImagePath = "spells/simple.png";
        }

        public override void Use(Entity user, params Entity[] targets)
        {
            if (user != null &&
                targets.First() != null)
            {
                var target = targets.First();
                var s = user.Stats;

                if (user.Name == "Denny")
                {
                    Console.WriteLine("Денни атаковал");
                }
                
                var modifier = user.Modifiers.Modifiers.FirstOrDefault(x => x.GetType() == typeof(IncreasedDamageModifier));
                var damage = (s.Strength * 2 + s.Strength * (s.Agility / 100));
                double additionalDamage = (damage * modifier.Value) / 100;

                damage += (int)Math.Round(additionalDamage);

                var resultDamage = target.TakeDamage(damage);

                string log = $"{user.Leading()} провёл комбинацию простых ударов  /i{ImagePath}/i " +
                    $"на {target.Leading()} и нанёс /b{resultDamage.Count}/b /bурона./b";

                _ = StartRest();
                SendBattleLog(log, user, targets.First());
            }
        }
    }
}
