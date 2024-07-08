﻿using Server.Models.EntityFramework;

namespace Server.Models.Handlers
{
    public class UserStatsHandler : StatsHandler
    {
        public int Level { get; private set; } = 1;
        public int CurrentExp { get; private set; } = 0;
        public int ToLevelExp { get; private set; } = 0;
        public int TotalPoints { get; private set; } = 0;
        public int FreePoints { get; private set; } = 5;
        public override int Strength { get; protected set; } = 5;
        public override int Agility { get; protected set; } = 5;
        public override int Intelligence { get; protected set; } = 5;

        public UserStatsHandler(Stats _stats)
        {
            CreateStats(_stats);
        }

        public void CreateStats(Stats stats)
        {
            CurrentExp = stats.CurrentExp;
            TotalPoints = stats.TotalPoints;
            FreePoints = stats.FreePoints;
            Strength = stats.Strength;
            Agility = stats.Agility;
            Intelligence = stats.Intelligence;

            var level = new LevelFactory().SetLevel(CurrentExp);

            Level = level.Key;
            ToLevelExp = level.Value;
        }

        public void AddExp(int exp)
        {
            CurrentExp += exp;

            if (CurrentExp >= ToLevelExp)
            {
                var newLvlPair = new LevelFactory().LevelUp(Level);

                Level = newLvlPair.Key;
                ToLevelExp = newLvlPair.Value;
            }
        }
    }
}
