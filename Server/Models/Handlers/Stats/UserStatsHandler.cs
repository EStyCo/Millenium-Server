using Server.Models.Utilities;
using Server.EntityFramework.Models;
using Server.Models.DTO.User;
using Server.Models.Entities;

namespace Server.Models.Handlers.Stats
{
    public class UserStatsHandler : StatsHandler
    {
        public int Level { get; set; } = 1;
        public int CurrentExp { get; set; } = 0;
        public int ToLevelExp { get; set; } = 0;
        public int TotalPoints { get; private set; } = 0;
        public int FreePoints { get; set; } = 5;
        public OriginalStatsValues OriginalStats { get; private set; }

        public override void SetStats(StatsEF stats)
        {
            CurrentExp = stats.CurrentExp;
            TotalPoints = stats.TotalPoints;
            FreePoints = stats.FreePoints;
            Strength = stats.Strength;
            Agility = stats.Agility;
            Vitality = stats.Vitality;
            Intelligence = stats.Intelligence;
            Mastery = stats.Mastery;
            Luck = stats.Luck;

            OriginalStats = new(stats);

            var level = new LevelFactory().SetLevel(CurrentExp);

            Level = level.Key;
            ToLevelExp = level.Value;
        }

        public void UpdateExp(int exp)
        {





            /*CurrentExp += exp;
            if (CurrentExp >= ToLevelExp)
            {
                var newLvlPair = new LevelFactory().LevelUp(Level);
                Level = newLvlPair.Key;
                ToLevelExp = newLvlPair.Value;

                (Entity as ActiveUser)?.AddBattleLog($"Вы достигли {Level} уровня. Мои поздравления!");
            }*/
        }

        public void ReturnToOriginalStats()
        {
            Strength = OriginalStats.Strength;
            Agility = OriginalStats.Agility;
            Vitality = OriginalStats.Vitality;
            Intelligence = OriginalStats.Intelligence;
            Mastery = OriginalStats.Mastery;
            Luck = OriginalStats.Luck;
        }

        public override StatDTO ToJson()
        {
            return new(this);
        }
    }
}