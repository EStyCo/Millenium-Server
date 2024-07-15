using Server.Models.DTO.Inventory;
using Server.Models.Utilities.Slots;

namespace Server.Models.Inventory
{
    public class Inventory
    {
        public List<Item> Items { get; private set; } = [];
        public Dictionary<SlotType, Item?> Slots { get; private set; }

        public Inventory(List<Item> items)
        {
            Items = items;
            Slots = new()
            {
                { SlotType.Head, null },
                { SlotType.Body, null },
                { SlotType.Feet, null },
                { SlotType.Weapon, null },
            };

            EquipItems();
        }

        private void EquipItems()
        {
            foreach (var item in Items)
            {
                if(item.IsEquipped)
                {
                    Slots[item.SlotType] = item;
                }
            }
        }

        public void AddItem(Item item)
        {
            Items.Add(item);
        }

        public void TakeItem(int id)
        {
            var item = FindItem(id);
            if (item != null)
            {
                Items.Remove(item);
            }
        }

        public void DeleteItem(int id)
        {
            var item = FindItem(id);
            if (item != null)
                Items.Remove(item);
        }

        private Item? FindItem(int id)
        {
            return Items.FirstOrDefault(x => x.Id == id);
        }

        public DressingResult Equip(int id)
        {
            var item = FindItem(id);
            if (item == null) return null;

            int? dressedItemId = UnEquip(item.SlotType);

            Slots[item.SlotType] = item;
            item.IsEquipped = true;

            return new(item.Id, dressedItemId);
        }

        public int? UnEquip(SlotType slotType)
        {
            //var slotPair = Slots.FirstOrDefault(x => x.Key == slotType);
            var slot = Slots[slotType];
            int id = -1;
            if (slot != null)
            {
                id = slot.Id;
                slot.IsEquipped = false;
                Slots[slotType] = null;
                return id;
            }
            return null;
        }

        public int? UnEquip(int id)
        {
            var item = FindItem(id);
            if (item == null) return null;

            var slot = Slots[item.SlotType];
            if (slot != null)
            {
                slot.IsEquipped = false;
                Slots[item.SlotType] = null;
                return item.Id;
            }

            return null;
        }

        public InventoryDTO ToJsonInventory()
        {
            return new(Items.Select(x => x.ToJson()).ToList());
        }

        public EquipmentDTO ToJsonEquip()
        {
            return new(
                Slots[SlotType.Head]?.ToJson(),
                Slots[SlotType.Body]?.ToJson(),
                Slots[SlotType.Feet]?.ToJson(),
                Slots[SlotType.Weapon]?.ToJson());
        }
    }
}
