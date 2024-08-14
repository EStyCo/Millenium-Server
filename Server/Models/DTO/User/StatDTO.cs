using Server.Models.Handlers.Stats;

namespace Server.Models.DTO.User
{
    public class StatDTO(UserStatsHandler stats)
    {
        public int Level { get; } = stats.Level;
        public int CurrentExp { get; } = stats.CurrentExp;
        public int ToLevelExp { get; } = stats.ToLevelExp;
        public int FreePoints { get; } = stats.FreePoints;
        public int Strength { get; } = stats.Strength;
        public int Agility { get; } = stats.Agility;
        public int Vitality { get; } = stats.Vitality;
        public int Intelligence { get; } = stats.Intelligence;
        public int Mastery { get; } = stats.Mastery;
        public int Luck { get; } = stats.Luck;
    }
}
