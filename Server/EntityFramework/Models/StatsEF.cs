using Server.Models.DTO.User;
using Server.Models.Utilities;

namespace Server.EntityFramework.Models
{
    public class StatsEF
    {
        public int Id { get; set; }
        public int CharacterEFId { get; set; }
        public CharacterEF CharacterEF { get; set; }
        public int CurrentExp { get; private set; } = 0;
        public int TotalPoints { get; private set; } = 0;
        public int FreePoints { get; private set; } = 5;
        public int Strength { get; set; } = 5;
        public int Agility { get; set; } = 5;
        public int Vitality { get; set; } = 5;
        public int Intelligence { get; set; } = 5;
        public int Mastery { get; set; } = 5;
        public int Luck { get; set; } = 5;

        public bool ChangeStats(UpdateStatDTO dto)
        {
            if (!StatValidator.CheckStats(this, dto)) return false;

            Strength = dto.Strength;
            Agility = dto.Agility;
            Vitality = dto.Vitality;
            Intelligence = dto.Intelligence;
            Mastery = dto.Mastery;
            Luck = dto.Luck;
            FreePoints = dto.FreePoints;

            return true;
        }

        public void ChangeExp(int currentExp)
        {
            CurrentExp += currentExp;
        }

        public void AddFreePoints(int count)
        {
            FreePoints += count;
        }
    }
}
