using Server.EntityFramework.Models;
using Server.Models.DTO.User;

namespace Server.Models.Utilities
{
    public static class StatValidator
    {
        public static bool CheckStats(StatsEF stats, UpdateStatDTO dto)
        {
            if (stats == null) 
                return false;
            int freePoints = stats.FreePoints;

            freePoints -= dto.Strength - stats.Strength;
            freePoints -= dto.Agility - stats.Agility;
            freePoints -= dto.Vitality - stats.Vitality;
            freePoints -= dto.Intelligence - stats.Intelligence;
            freePoints -= dto.Mastery - stats.Mastery;
            freePoints -= dto.Luck - stats.Luck;

            if (freePoints < 0) 
                return false;
            return true;
        }
    }
}