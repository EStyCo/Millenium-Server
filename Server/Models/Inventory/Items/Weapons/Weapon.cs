using Server.Models.Utilities.Slots;

namespace Server.Models.Inventory.Items.Weapon
{
    public abstract class Weapon : Item
    {
        public abstract int Damage { get; }
    }
}
