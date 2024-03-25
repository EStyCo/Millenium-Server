using Server.Models.DTO;

namespace Server.Models
{
    public static class StatValidator
    {
        public static bool CheckStats(Character character, UpdateStatDTO dto)
        { 
            if(character == null) return false;

            int freePoints = character.FreePoints;

            freePoints -= (dto.Strength - character.Strength);
            freePoints -= (dto.Agility - character.Agility);
            freePoints -= (dto.Intelligence - character.Intelligence);

            if (freePoints < 0) return false;

            return true;
        }
    }
}
