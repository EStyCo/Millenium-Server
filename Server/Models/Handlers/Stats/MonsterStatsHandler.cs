namespace Server.Models.Handlers.Stats
{
    public class MonsterStatsHandler : StatsHandler
    {
        public override int Strength { get; set; }
        public override int Agility { get; set; }
        public override int Intelligence { get; set; }

        public MonsterStatsHandler(int strength, int agility, int intelligence)
        {
            Strength = strength;
            Agility = agility;
            Intelligence = intelligence;
        }
    }
}
