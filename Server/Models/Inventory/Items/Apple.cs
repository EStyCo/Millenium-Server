using Server.Models.Entities;
using Server.Models.Utilities.Slots;

namespace Server.Models.Inventory.Items
{
    public class Apple : Item
    {
        public override int Id { get; set; }
        public override SlotType SlotType { get; } = SlotType.Weapon;
        public override ItemType ItemType { get; } = ItemType.Apple;
        public override string Name { get; } = "Яблоко";
        public override bool CanEquipped { get; } = true;
        public override string Description { get; } = "Просто вкусное яблоко.";
        public override string GainsDescription { get; } = "";
        public override string ImagePath { get; } = "items/apple.png";

        public override void ApplyChanges(ActiveUser user)
        {
            
        }
    }
}