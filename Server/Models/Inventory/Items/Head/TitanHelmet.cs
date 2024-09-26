using Server.Models.Utilities.Slots;

namespace Server.Models.Inventory.Items.Head
{
    public class TitanHelmet : Item
    {
        public override int Id { get; set; }
        public override SlotType SlotType { get; } = SlotType.Head;
        public override ItemType ItemType { get; } = ItemType.TitanHelmet;
        public override string Name { get; } = "Шлем Титана";
        public override bool CanEquipped { get; } = true;
        public override string Description { get; } = "Хорошо защищает голову, не зря их носили лучшие бойцы поколения";
        public override string GainsDescription { get; } = "";
        public override string ImagePath { get; } = "items/titanHelmet.png";

        public override void ApplyChanges(ActiveUser user)
        {
            
        }
    }
}
