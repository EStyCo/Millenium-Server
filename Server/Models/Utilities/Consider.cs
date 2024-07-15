using Server.Models.Handlers.Stats;
using Server.Models.Handlers.Vitality;

namespace Server.Models.Utilities
{
    public static class Consider
    {
        public static int MaxHP(UserStatsHandler stats)
        {
            return (int)(100 + (stats.Strength * 0.20) + (stats.Strength + stats.Agility + stats.Level));
        }

        public static int MaxMP(UserStatsHandler stats)
        {
            return (int)(100 + (stats.Intelligence * 0.20) + (stats.Intelligence + stats.Agility + stats.Level));
        }

        public static int RegenRateHP(UserVitalityHandler v, UserStatsHandler s)
        {
            var res = (v.MaxHP / 60.0) * 0.5;
            return (int)Math.Round(res);
        }

        public static int RegenRateMP(UserStatsHandler character)
        {
            return new Random().Next(1, 5);
        }
    }
}
