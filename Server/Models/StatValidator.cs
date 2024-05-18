using Server.Controllers;
using Server.Models.DTO;
using Server.Models.EntityFramework;
using Server.Models.Handlers;

namespace Server.Models
{
    public static class StatValidator
    {
        public static bool CheckStats(Stats stats, UpdateStatDTO dto)
        { 
            if(stats == null) return false;

            int freePoints = stats.FreePoints;

            freePoints -= (dto.Strength - stats.Strength);
            freePoints -= (dto.Agility - stats.Agility);
            freePoints -= (dto.Intelligence - stats.Intelligence);

            if (freePoints < 0) return false;

            return true;
        }
    }
}
