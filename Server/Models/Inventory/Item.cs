using Server.Models.DTO.Inventory;
using Server.Models.Utilities.Slots;

namespace Server.Models.Inventory
{
    public abstract class Item
    {
        public abstract int Id { get; set; }
        public abstract SlotType SlotType { get; }
        public abstract ItemType ItemType { get; }
        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract string ImagePath { get; }
        public abstract bool CanEquipped { get; }
        public virtual bool IsEquipped { get; set; } = false;

        public abstract void ApplyChanges();

        public virtual ItemDTO ToJson()
        {
            return new(Id, SlotType, ItemType, Name, Description, ImagePath, CanEquipped, IsEquipped);
        }
    }
}
