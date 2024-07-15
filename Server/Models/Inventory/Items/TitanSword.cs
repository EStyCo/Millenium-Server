using Server.Models.Utilities.Slots;

namespace Server.Models.Inventory.Items
{
    public class TitanSword : Item
    {
        public override int Id { get; set; }
        public override SlotType SlotType { get; } = SlotType.Weapon;
        public override ItemType ItemType { get; } = ItemType.TitanSword;
        public override string Name { get; } = "Меч Титана";
        public override bool CanEquipped { get; } = true;
        public override string Description { get; } = "Оружие Титанов, слегка ржавый, но все же очень острый.";
        public override string ImagePath { get; } = "items/titanSword.png";

        public override void ApplyChanges()
        {
            throw new NotImplementedException();
        }
    }
}

