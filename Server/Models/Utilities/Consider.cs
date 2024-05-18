using Server.Models.DTO;
using Server.Models.EntityFramework;
using Server.Models.Handlers;

namespace Server.Models.Utilities
{
    public static class Consider
    {
        public static int MaxHP(StatsHandler stats)
        {
            return (int)(100 + (stats.Strength * 0.20) + (stats.Strength + stats.Agility + stats.Level));
        }

        public static int MaxMP(StatsHandler stats)
        {
            return (int)(100 + (stats.Intelligence * 0.20) + (stats.Intelligence + stats.Agility + stats.Level));
        }

        public static int RegenRateHP(StatsHandler character)
        {
            return new Random().Next(1, 10);
        }

        public static int RegenRateMP(StatsHandler character)
        {
            return new Random().Next(1, 10);
        }
    }
}
