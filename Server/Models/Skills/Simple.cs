using Server.Models.DTO;

namespace Server.Models.Skills
{
    public class Simple
    {
        public string Name { get; set; } = string.Empty;
        public int CoolDown { get; set; }
        public int RestSeconds { get; set; } = 0;
        public bool IsReady { get; set; } = true;
        public Simple()
        {
            Name = "Удар с правой";
            CoolDown = 7;
        }

        public int Attack(CharacterDTO c)
        {
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

            while (RestSeconds > 0)
            { 
                await Task.Delay(1000);
                RestSeconds--;
            }

            IsReady = true;
        }
    }
}
