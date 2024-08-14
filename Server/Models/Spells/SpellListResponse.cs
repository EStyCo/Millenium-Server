using Server.Models.Interfaces;

namespace Server.Models.Spells
{
    public class SpellListResponse
    {
        public bool CanAttack { get; }
        public int GlobalRestSeconds { get; } 
        public List<Spell> SpellList { get; }

        public SpellListResponse(ActiveUser user)
        {
            CanAttack = user.CanAttack;
            GlobalRestSeconds = user.GlobalRestSeconds;
            SpellList = user.ActiveSkills;
        }
    }
}
