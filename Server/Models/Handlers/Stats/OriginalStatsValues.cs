using Server.EntityFramework.Models;

namespace Server.Models.Handlers.Stats
{
    public class OriginalStatsValues(StatsEF stats)
    {
        public int Strength { get; } = stats.Strength;
        public int Agility { get; } = stats.Agility;
        public int Vitality { get; } = stats.Vitality;
        public int Intelligence { get; } = stats.Intelligence;
        public int Mastery { get; } = stats.Mastery;
        public int Luck { get; } = stats.Luck;
    }
}
