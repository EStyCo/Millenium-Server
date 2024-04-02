using Server.Models.DTO;
using static Server.Models.ActiveUser;

namespace Server.Models.Skills
{
    public class Simple : Skill
    {
        public Simple()
        {
            Name = "Удар с правой";
            Description = "Обычный удар с правой, ничего выдающегося.";
            ImagePath = "spell_simple.png";
            CoolDown = 7;
        }

        public async override Task<int> Attack(CharacterDTO c)
        {
            await Task.Delay(1);
            if (IsReady)
            {
                StartRest();
                return (c.Strength * 2) + c.Strength * (c.Agility / 100);
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
        }
    }
}
