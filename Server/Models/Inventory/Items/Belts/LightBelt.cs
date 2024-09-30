using Server.Models.Modifiers.Additional.Stats;
using Server.Models.Modifiers.Increased;
using Server.Models.Utilities.Slots;

namespace Server.Models.Inventory.Items.Belts
{
    public class LightBelt : Item
    {
        public override int Id { get; set; }
        public override SlotType SlotType { get; } = SlotType.Belt;
        public override ItemType ItemType { get; } = ItemType.LightBelt;
        public override string Name { get; } = "Лёгкий пояс";
        public override bool CanEquipped { get; } = true;
        public override string Description { get; } = "Поясок, которым можно подпоясаться.";
        public override string GainsDescription { get; } = "Дополнительное мастерство +15\nУвеличение урона +3%";
        public override string ImagePath { get; } = "items/lightBelt.png";

        public override void ApplyChanges(ActiveUser user)
        {
            var masteryMdf = user.Modifiers.Get<AddMastery>();
            var incDmgMdf = user.Modifiers.Get<IncreasedDamageModifier>();

            if (masteryMdf != null && incDmgMdf != null)
            {
                masteryMdf.Value += 15;
                incDmgMdf.Value += 3;
            }
        }
    }
}
