using Server.Models.Utilities.Slots;

namespace Server.Models.Inventory.Items
{
    public class Stone : Item
    {
        public override int Id { get; set; }
        public override SlotType SlotType { get; } = SlotType.Head;
        public override ItemType ItemType { get; } = ItemType.Stone;
        public override string Name { get; } = "Красивый камень";
        public override bool CanEquipped { get; } = false;
        public override string Description { get; } = "Определённо выделяется на фоне остальных, не понятно только чем же..";
        public override string GainsDescription { get; } = "";
        public override string ImagePath { get; } = "simpleItems/stone.png";

        public override void ApplyChanges(ActiveUser user)
        {
            
        }
    }
}
