using Server.Models.Modifiers.Additional;
using Server.Models.Modifiers.Additional.Stats;
using Server.Models.Utilities.Slots;

namespace Server.Models.Inventory.Items.Body
{
    public class TitanArmor : Item
    {
        public override int Id { get; set; }
        public override SlotType SlotType { get; } = SlotType.Body;
        public override ItemType ItemType { get; } = ItemType.TitanArmor;
        public override string Name { get; } = "Латы Титана";
        public override bool CanEquipped { get; } = true;
        public override string Description { get; } = "Их носили великие полководцы.";
        public override string GainsDescription { get; } = "Сила +15\nЛовкость +15\nАура лечения восстанавливает дополнительные 2 здоровья.";
        public override string ImagePath { get; } = "items/titanArmor.png";

        public override void ApplyChanges(ActiveUser user)
        {
            user.Modifiers.Get<AddStrength>().Value += 15;
            user.Modifiers.Get<AddAgility>().Value += 15;
            user.Modifiers.Get<AddRegeratedHP>().Value += 2;
        }
    }
}
