using Server.Models.DTO;

namespace Server.Models.Utilities
{
    public static class Consider
    {
        public static int MaxHP(Character character)
        {
            return (int)(100 + (character.Strength * 0.20) + (character.Strength + character.Agility + character.Level));
        }

        public static int MaxMP(Character character)
        {
            return (int)(100 + (character.Intelligence * 0.20) + (character.Intelligence + character.Agility + character.Level));
        }

        public static int RegenRateHP(Character character)
        {
            return new Random().Next(1, 10);
        }

        public static int RegenRateMP(Character character)
        {
            return new Random().Next(1, 10);
        }

    }
}
