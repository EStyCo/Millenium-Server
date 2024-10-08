﻿using Server.Models.Entities;
using Server.Models.Spells.States;
using Server.Models.Utilities;

namespace Server.Models.Spells.Models
{
    public class Bleeding : Spell
    {
        private const int BLEEDING_CHANCE = 65;

        public Bleeding()
        {
            SpellType = SpellType.Bleeding;
            Name = "Рассечение";
            CoolDown = 45;
            Description = "Наносит не высокий урон, с 25% шансом накладывает кровотечение.";
            ImagePath = "spells/bleeding.png";
        }

        public override void Use(Entity user, params Entity[] targets)
        {
            if (user == null && targets.First() == null) return;

            var target = targets.First();
            var s = user.Stats;
            var damage = (s.Strength * 2 + s.Strength * (s.Agility / 100)) / 2.5;

            if (new Random().Next(100) < BLEEDING_CHANCE)
            {
                target.AddState<BleedingState>(user);
            }

            var resultDamage = target.TakeDamage((int)damage);

            string log = $"{user.Leading()} коварно подрезал /i{ImagePath}/i {target.Leading()} и нанёс /b{resultDamage.Count}/b /bурона./b";

            _ = StartRest();
            SendBattleLog(log, user, target);
        }
    }
}
