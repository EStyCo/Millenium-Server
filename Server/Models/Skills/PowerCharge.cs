using Server.Models.DTO;
using Server.Models.Utilities;
using static Server.Models.ActiveUser;

namespace Server.Models.Skills
{
    public class PowerCharge : Spell
    {
        public PowerCharge()
        {
            SpellType = SpellType.PowerCharge;
            Name = "Сильный удар";
            CoolDown = 15;
            Description = "За мощь, нужно платить временем.";
            ImagePath = "spell_simple.png";
        }

        public override Task Use(Entity user, params Entity[] target)
        {
            return Task.CompletedTask;
        }


        /*public async override Task<int> Attack(CharacterDTO c)
        {
            await Task.Delay(1);
            if (IsReady)
            {
                StartRest();
                return ((c.Strength * 2) + c.Strength * (c.Agility / 100)) * 2;
            }

            return 0;
        }

        public async Task StartRest()
        {
            IsReady = false;
            RestSeconds = CoolDown;
            SendRestDelegate?.Invoke(Id, RestSeconds);

            while (RestSeconds > 0)
            {
                await Task.Delay(1000);
                RestSeconds--;
                SendRestDelegate?.Invoke(Id, RestSeconds);
            }

            IsReady = true;
            SendRestDelegate?.Invoke(Id, RestSeconds);
        }*/

    }
}
