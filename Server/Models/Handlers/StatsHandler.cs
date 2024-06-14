namespace Server.Models.Handlers
{
    public abstract class StatsHandler
    {
        public abstract int Strength { get; protected set; }
        public abstract int Agility { get; protected set; }
        public abstract int Intelligence { get; protected set; }
    }
}
