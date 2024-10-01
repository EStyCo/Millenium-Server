using Server.Models.Entities;
using Server.Models.Modifiers.Additional;
using Server.Models.Modifiers.Additional.Stats;
using Server.Models.Modifiers.Unique;
using Server.Models.Utilities.Slots;

namespace Server.Models.Inventory.Items.Weapon
{
    public class TitanSword : Weapon
    {
        public override int Id { get; set; }
        public override SlotType SlotType { get; } = SlotType.Weapon;
        public override ItemType ItemType { get; } = ItemType.TitanSword;
        public override string Name { get; } = "Меч Титана";
        public override bool CanEquipped { get; } = true;
        public override string Description { get; } = "Оружие титанов, слегка ржавый, но еще очень острый. Готов служить новому хозяину верой и правдой.";
        public override string GainsDescription { get; } = "Сила +20\nЗдоровье +35\nПовышает урон Сильного удара.";
        public override string ImagePath { get; } = "items/titanSword.png";
        public override int Damage { get; } = 35;

        public override void ApplyChanges(ActiveUser user)
        {
            var addStrength = user.Modifiers.Get<AddStrength>();
            var addHP = user.Modifiers.Get<AdditionalHPModifier>();
            var addDamagePowerCharge = user.Modifiers.Get<IncreasedDamagePowerChargeModifier>();

            addStrength.Value += 20;
            addHP.Value += 35;
            addDamagePowerCharge.Value += 50;
        }
    }
}

