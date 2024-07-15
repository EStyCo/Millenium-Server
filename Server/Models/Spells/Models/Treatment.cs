using Server.Models.Spells.States;
using Server.Models.Utilities;

namespace Server.Models.Spells.Models
{
    public class Treatment : Spell
    {
        public Treatment()
        {
            SpellType = SpellType.Treatment;
            Name = "Аура лечения";
            CoolDown = 35;
            Description = "Временно усиливает регенерацию здоровья";
            ImagePath = "spells/treatment.png";
        }

        public override void Use(Entity _user, params Entity[] targets)
        {
            var user = _user as ActiveUser;
            if (user == null) return;
            user.AddState<TreatmentState>(user);
            user.UpdateStates();

            _ = StartRest();
            /*string log = $"{user.Name} подрезал {target.Name} и нанёс {resultDamage.Count}. [{resultDamage.CurrentHP}/{resultDamage.MaxHP}]";
            SendBattleLog(log, user, target);*/
        }
    }
}
