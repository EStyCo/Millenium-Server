using Server.Models.DTO;

namespace Server.Models.EntityFramework
{
    public class Stats
    {
        public int CharacterId { get; set; }
        public Character Character { get; set; }
        public int Level { get; private set; } = 1;
        public int CurrentExp { get; private set; } = 0;
        public int ToLevelExp { get; private set; } = 0;
        public int TotalPoints { get; private set; } = 0;
        public int FreePoints { get; private set; } = 5;
        public int Strength { get; private set; } = 5;
        public int Agility { get; private set; } = 5;
        public int Intelligence { get; private set; } = 5;

        public bool ChangeStats(UpdateStatDTO dto)
        {
            if (!StatValidator.CheckStats(this, dto)) return false;

            Strength = dto.Strength;
            Agility = dto.Agility;
            Intelligence = dto.Intelligence;
            FreePoints = dto.FreePoints;

            return true;
        }

        public void ChangeExp(int currentExp)
        {
            CurrentExp += currentExp;
        }
    }
}
