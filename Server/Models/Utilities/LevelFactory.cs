namespace Server.Models.Utilities
{
    public class LevelFactory
    {
        public Dictionary<int, int> LevelCountExp { get; } = new Dictionary<int, int>()
        {
            { 1, 15 },
            { 2, 50 },
            { 3, 250 },
            { 4, 500 },
            { 5, 1350 },
            { 6, 3600 },
            { 7, 5200 },
            { 8, 8000 },
            { 9, 13550 },
        };

        public KeyValuePair<int, int> SetLevel(int currentExp)
        {
            var smallerOrEqual = LevelCountExp
                .Where(pair => pair.Value > currentExp)
                .ToDictionary(pair => pair.Key, pair => pair.Value);

            if (!smallerOrEqual.Any())
            {
                return new KeyValuePair<int, int>(1, 15);
            }

            return smallerOrEqual.First();
        }

        public KeyValuePair<int, int> LevelUp(int currentLevel)
        {
            var nextLvlPair = LevelCountExp.FirstOrDefault(pair => pair.Key > currentLevel);

            if (nextLvlPair.Equals(default(KeyValuePair<int, int>)))
            {
                return new KeyValuePair<int, int>(1, 0);
            }

            return nextLvlPair;
        }
    }
}
