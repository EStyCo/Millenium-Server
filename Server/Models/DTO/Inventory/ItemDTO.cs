using Server.Models.Inventory;
using Server.Models.Inventory.Items.Weapon;
using Server.Models.Utilities.Slots;

namespace Server.Models.DTO.Inventory
{
    public class ItemDTO
    {
        public int Id { get; }
        public SlotType SlotType { get; }
        public ItemType ItemType { get; }
        public string Name { get; }
        public string Description { get; }
        public string GainsDescription { get; }
        public string ImagePath { get; }
        public bool CanEquipped { get; }
        public bool IsEquipped { get; }
        public int? Damage { get; }

        public ItemDTO(Item item)
        {
            Id = item.Id;
            SlotType = item.SlotType;
            ItemType = item.ItemType;
            Name = item.Name;
            Description = item.Description;
            GainsDescription = item.GainsDescription;
            ImagePath = item.ImagePath;
            CanEquipped = item.CanEquipped;
            IsEquipped = item.IsEquipped;

            var weapon = item as Weapon;
            Damage = weapon?.Damage;
        }
    }
}
