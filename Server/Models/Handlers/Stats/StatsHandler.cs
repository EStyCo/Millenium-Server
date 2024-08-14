using Server.EntityFramework.Models;
using Server.Models.DTO.User;

namespace Server.Models.Handlers.Stats
{
    public abstract class StatsHandler
    {
        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Vitality { get; set; }
        public int Intelligence { get; set; }
        public int Mastery { get; set; }
        public int Luck { get; set; }

        public abstract void SetStats(StatsEF stats);

        public abstract StatDTO ToJson();
    }
}
