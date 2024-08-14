using Server.EntityFramework.Models;
using Server.Models.DTO.User;

namespace Server.Models.Handlers.Stats
{
    public class MonsterStatsHandler : StatsHandler
    {
        public MonsterStatsHandler(
            int strength, int agility, int vitality, 
            int intelligence, int mastery, int luck)
        {
            Strength = strength;
            Agility = agility;
            Vitality = vitality;
            Intelligence = intelligence;
            Mastery = mastery;
            Luck = luck;
        }

        public override void SetStats(StatsEF stats)
        {
            throw new NotImplementedException();
        }

        public override StatDTO ToJson()
        {
            throw new NotImplementedException();
        }
    }
}
