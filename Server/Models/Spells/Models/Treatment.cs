using Server.Models.Spells.States;
using Server.Models.Utilities;

namespace Server.Models.Spells.Models
{
    public class Treatment : Spell
    {
        public Treatment()
        {
            Name = "Аура лечения";
            SpellType = SpellType.Treatment;
            SelfUse = true;
            CoolDown = 6;
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
