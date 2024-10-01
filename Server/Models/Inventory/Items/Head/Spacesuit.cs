using Server.Models.Entities;
using Server.Models.Modifiers.Additional;
using Server.Models.Modifiers.Unique;
using Server.Models.Utilities.Slots;

namespace Server.Models.Inventory.Items.Head
{
    public class Spacesuit : Item
    {
        public override int Id { get; set; }
        public override SlotType SlotType { get; } = SlotType.Head;
        public override ItemType ItemType { get; } = ItemType.Spacesuit;
        public override string Name { get; } = "Скафандр";
        public override bool CanEquipped { get; } = true;
        public override string Description { get; } = "Такое обычно носят астронавты и Даники-дураки.";
        public override string GainsDescription { get; } = "Дополнительное здоровье +25";
        public override string ImagePath { get; } = "items/helmet.png";

        public override void ApplyChanges(ActiveUser user)
        {
            var addHP = user.Modifiers.Get<AdditionalHPModifier>();

            addHP.Value += 25;
        }
    }
}
