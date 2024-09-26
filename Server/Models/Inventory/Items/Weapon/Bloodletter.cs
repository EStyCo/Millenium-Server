using Server.Models.Modifiers.Additional;
using Server.Models.Modifiers.Unique;
using Server.Models.Utilities.Slots;

namespace Server.Models.Inventory.Items.Weapon
{
    public class Bloodletter : Item
    {
        public override int Id { get; set; }
        public override SlotType SlotType { get; } = SlotType.Weapon;
        public override ItemType ItemType { get; } = ItemType.TitanSword;
        public override string Name { get; } = "Кровопускатель";
        public override bool CanEquipped { get; } = true;
        public override string Description { get; } = "Лёгкий кинжал, волнистой формы, оставляет после себя кровоточащие раны.";
        public override string GainsDescription { get; } = "Дополнительный шанс наложения кровотечения +15%\nЛовкость +10\nСила +5\nШанс наложения кровотечения удачлив";
        public override string ImagePath { get; } = "items/bloodletter.png";

        public override void ApplyChanges(ActiveUser user)
        {
            var addBleedingChance = user.Modifiers.Get<AdditionalBleedingChance>();
            var addStrength = user.Modifiers.Get<AdditionalStrength>();
            var addAgility = user.Modifiers.Get<AdditionalAgility>();
            var luckBleed = user.Modifiers.Get<LuckyBleedingChance>();

            addBleedingChance.Value += 15;
            addStrength.Value += 5;
            addAgility.Value += 10;
            luckBleed.Value = 1;
        }
    }
}
