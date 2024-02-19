using Server.Models.DTO;

namespace Server.Models.Utilities
{
    public static class Consider
    {
        public static int MaxHP(CharacterDTO character)
        {
            return 200;
        }

        public static int MaxMP(CharacterDTO character)
        {
            return 200;
        }

        public static int RegenRateHP(CharacterDTO character)
        {
            return new Random().Next(1, 10);
        }

        public static int RegenRateMP(CharacterDTO character)
        {
            return new Random().Next(1, 10);
        }

    }
}
