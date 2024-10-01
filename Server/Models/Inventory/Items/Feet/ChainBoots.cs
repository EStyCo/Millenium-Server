using Server.Models.Entities;
using Server.Models.Utilities.Slots;

namespace Server.Models.Inventory.Items.Feet
{
    public class ChainBoots : Item
    {
        public override int Id { get; set; }
        public override SlotType SlotType { get; } = SlotType.Feet;
        public override ItemType ItemType { get; } = ItemType.ChainBoots;
        public override string Name { get; } = "Кольчужные сапоги";
        public override bool CanEquipped { get; } = true;
        public override string Description { get; } = "Обычные сапоги из кольчуги, тяжеловаты, но свое дело делают.";
        public override string GainsDescription { get; } = "";
        public override string ImagePath { get; } = "items/chainBoots.png";

        public override void ApplyChanges(ActiveUser user)
        {
            
        }
    }
}
