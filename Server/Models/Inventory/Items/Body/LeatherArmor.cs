using Server.Models.Modifiers.Additional;
using Server.Models.Modifiers.Increased;
using Server.Models.Utilities.Slots;

namespace Server.Models.Inventory.Items.Body
{
    public class LeatherArmor : Item
    {
        public override int Id { get; set; }
        public override SlotType SlotType { get; } = SlotType.Body;
        public override ItemType ItemType { get; } = ItemType.LeatherArmor;
        public override string Name { get; } = "Кожаная броня";
        public override bool CanEquipped { get; } = true;
        public override string Description { get; } = "Такое обмундирование можно увидеть у разбойников, в ней легко догнать - просто убежать.";
        public override string GainsDescription { get; } = "Дополнительное здоровье +100\nУвеличение урона и здоровья +5%";
        public override string ImagePath { get; } = "items/leatherArmor.png";

        public override void ApplyChanges(ActiveUser user)
        {
            var dmgModifier = user.Modifiers.Get<IncreasedDamageModifier>();
            var addHpModifier = user.Modifiers.Get<AdditionalHPModifier>();
            var incHpModifier = user.Modifiers.Get<IncreasedHPModifier>();

            if (dmgModifier != null && addHpModifier != null && incHpModifier != null)
            {
                dmgModifier.Value += 5;
                addHpModifier.Value += 100;
                incHpModifier.Value += 5;
            }
        }
    }
}
