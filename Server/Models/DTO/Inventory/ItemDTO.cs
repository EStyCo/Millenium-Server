using Server.Models.Inventory;
using Server.Models.Utilities.Slots;

namespace Server.Models.DTO.Inventory
{
    public class ItemDTO(Item item)
    {
        public int Id { get; } = item.Id;
        public SlotType SlotType { get; } = item.SlotType;
        public ItemType ItemType { get; } = item.ItemType;
        public string Name { get; } = item.Name;
        public string Description { get;  } = item.Description;
        public string GainsDescription { get;  } = item.GainsDescription;
        public string ImagePath { get;  } = item.ImagePath;
        public bool CanEquipped { get;  } = item.CanEquipped;
        public bool IsEquipped { get;  } = item.IsEquipped;
    }
}
