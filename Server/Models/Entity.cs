using Server.Models.Utilities;

namespace Server.Models
{
    public abstract class Entity
    {
        public abstract string Name { get; protected set; }
        public abstract BaseStatsHandler Stats { get; protected set; }
        public abstract void UseSkill(SpellType type, params Entity[] target);
        public abstract int TakeDamage(int damage);
    }
}
