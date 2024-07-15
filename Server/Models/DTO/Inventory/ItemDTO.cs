using Server.Models.Utilities.Slots;

namespace Server.Models.DTO.Inventory
{
    public class ItemDTO
    {
        public int Id { get; set; }
        public SlotType SlotType { get; set; }
        public ItemType ItemType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public bool CanEquipped { get; set; }
        public bool IsEquipped { get; set; }

        public ItemDTO(int id, SlotType slotType, ItemType itemType, string name, string description, string imagePath, bool canEquipped, bool isEquipped)
        {
            Id = id;
            SlotType = slotType;
            ItemType = itemType;
            Name = name;
            Description = description;
            ImagePath = imagePath;
            CanEquipped = canEquipped;
            IsEquipped = isEquipped;
        }
    }
}
