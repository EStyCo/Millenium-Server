using Server.Models.Utilities.Slots;

namespace Server.Models.Inventory.Items
{
    public class Spacesuit : Item
    {
        public override int Id { get; set; }
        public override SlotType SlotType { get; } = SlotType.Head;
        public override ItemType ItemType { get; } = ItemType.Spacesuit;
        public override string Name { get; } = "Скафандр";
        public override bool CanEquipped { get; } = true;
        public override string Description { get; } = "Такое обычно носят астронавты и Даники-дураки.";
        public override string ImagePath { get; } = "items/helmet.png";

        public override void ApplyChanges()
        {
            throw new NotImplementedException();
        }
    }
}
