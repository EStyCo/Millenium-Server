namespace Server.Models.Spells
{
    public class SpellListResponse(bool canAttack, int globalRestSeconds, List<Spell> list)
    {
        public bool CanAttack { get; } = canAttack;
        public int GlobalRestSeconds { get; } = globalRestSeconds;
        public List<Spell> SpellList { get; } = list;
    }
}
