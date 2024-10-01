using Server.Models.Utilities;
using Server.Models.Entities;

namespace Server.Hubs.Locations.Interfaces
{
    public interface IBattleUsers
    {
        public void AttackUser(ActiveUser user, ActiveUser target, SpellType type);
    }
}
