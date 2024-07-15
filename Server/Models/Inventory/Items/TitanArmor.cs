using Server.Models.Utilities.Slots;

namespace Server.Models.Inventory.Items
{
    public class TitanArmor : Item
    {
        public override int Id { get; set; }
        public override SlotType SlotType { get; } = SlotType.Body;
        public override ItemType ItemType { get; } = ItemType.TitanArmor;
        public override string Name { get; } = "Латы Титана";
        public override bool CanEquipped { get; } = true;
        public override string Description { get; } = "Их носили великие полководцы.";
        public override string ImagePath { get; } = "items/titanArmor.png";

        public override void ApplyChanges()
        {
            throw new NotImplementedException();
        }
    }
}
