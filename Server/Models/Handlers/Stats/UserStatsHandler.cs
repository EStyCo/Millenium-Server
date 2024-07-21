using Server.Models.Utilities;
using EF = Server.EntityFramework.Models;

namespace Server.Models.Handlers.Stats
{
    public class UserStatsHandler : StatsHandler
    {
        public int Level { get; private set; } = 1;
        public int CurrentExp { get; private set; } = 0;
        public int ToLevelExp { get; private set; } = 0;
        public int TotalPoints { get; private set; } = 0;
        public int FreePoints { get; private set; } = 5;

        public OriginalStatsValues OriginalStats { get; private set; }

        public override int Strength { get; set; }
        public override int Agility { get;  set; }
        public override int Intelligence { get; set; }

        public UserStatsHandler(EF.Stats stats)
        {
            CreateStats(stats);
            ResetToOriginalStats(stats);
        }

        public void CreateStats(EF.Stats stats)
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

        public void ResetToOriginalStats(EF.Stats stats)
        {
            OriginalStats = new OriginalStatsValues(stats.Strength, stats.Agility, stats.Intelligence);
        }
    }
}

public class OriginalStatsValues
{
    public int Strength { get; set; }
    public int Agility { get; set; }
    public int Intelligence { get; set; }

    public OriginalStatsValues(int strength, int agility, int intelligence)
    {
        Strength = strength;
        Agility = agility;
        Intelligence = intelligence;
    }
}