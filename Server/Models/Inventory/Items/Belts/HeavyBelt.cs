using Server.Models.Modifiers.Additional;
using Server.Models.Modifiers.Additional.Stats;
using Server.Models.Modifiers.Increased;
using Server.Models.Utilities.Slots;

namespace Server.Models.Inventory.Items.Belts
{
    public class HeavyBelt : Item
    {
        public override int Id { get; set; }
        public override SlotType SlotType { get; } = SlotType.Belt;
        public override ItemType ItemType { get; } = ItemType.HeavyBelt;
        public override string Name { get; } = "Тяжёлый пояс";
        public override bool CanEquipped { get; } = true;
        public override string Description { get; } = "Добротный крепкий пояс.";
        public override string GainsDescription { get; } = "Дополнительное здоровье +50 +15\nУвеличение урона +2%\bЛовкость +10";
        public override string ImagePath { get; } = "items/heavyBelt.png";

        public override void ApplyChanges(ActiveUser user)
        {
            var addAgility = user.Modifiers.Get<AddAgility>();
            var incDmgMdf = user.Modifiers.Get<IncreasedDamageModifier>();
            var addHp = user.Modifiers.Get<AdditionalHPModifier>();

            if (addAgility != null && incDmgMdf != null && addHp != null)
            {
                addHp.Value += 50;
                incDmgMdf.Value += 2;
                addAgility.Value += 10;
            }
        }
    }
}
