namespace Server.Models.Handlers.Stats
{
    public abstract class StatsHandler
    {
        public abstract int Strength { get; set; }
        public abstract int Agility { get; set; }
        public abstract int Intelligence { get; set; }
    }
}
