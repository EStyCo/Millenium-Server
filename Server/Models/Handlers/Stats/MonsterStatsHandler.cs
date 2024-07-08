namespace Server.Models.Handlers.Stats
{
    public class MonsterStatsHandler : StatsHandler
    {
        public override int Strength { get; protected set; }
        public override int Agility { get; protected set; }
        public override int Intelligence { get; protected set; }

        public MonsterStatsHandler(int strength, int agility, int intelligence)
        {
            Strength = strength;
            Agility = agility;
            Intelligence = intelligence;
        }
    }
}
