﻿namespace Server.Models.Utilities
{
    public class LevelFactory
    {
        public Dictionary<int, int> LevelCountExp { get; } = new Dictionary<int, int>()
        {
            { 1, 50 },
            { 2, 180 },
            { 3, 350 },
            { 4, 750 },
            { 5, 1450 },
            { 6, 4300 },
            { 7, 6200 },
            { 8, 9000 },
            { 9, 14550 },
            { 10, 23550 },
            { 11, 38000 },
            { 12, 44550 },
            { 13, 63000 },
            { 14, 90300 },
            { 15, 140000 },
            { 16, 185000 },
            { 17, 265000 },
            { 18, 313000 },
            { 19, 367000 },
            { 20, 410000 },
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
