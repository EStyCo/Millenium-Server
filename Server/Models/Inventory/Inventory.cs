using Server.Models.DTO.Inventory;
using Server.Models.Entities;
using Server.Models.Utilities.Slots;

namespace Server.Models.Inventory
{
    public class Inventory
    {
        private readonly ActiveUser user;
        public List<Item> Items { get; private set; } = [];
        public Dictionary<SlotType, Item?> Slots { get; private set; }

        public Inventory(List<Item> items, ActiveUser _user)
        {
            Items = items;
            user = _user;
            Slots = new()
            {
                { SlotType.Head, null },
                { SlotType.Body, null },
                { SlotType.Belt, null },
                { SlotType.Feet, null },
                { SlotType.Weapon, null },
            };
        }

        public void EquipItems()
        {
            foreach (var item in Items)
            {
                if (item.IsEquipped)
                {
                    Slots[item.SlotType] = item;
                    item.ApplyChanges(user);
                }
            }
        }

        public void AddItem(Item item)
        {
            Items.Add(item);
        }

        public void DeleteItem(int id)
        {
            var item = FindItem(id);
            if (item != null)
            {
                UnEquip(id);
                Items.Remove(item);
            }
        }

        private Item? FindItem(int id)
        {
            return Items.FirstOrDefault(x => x.Id == id);
        }

        public DressingResult? Equip(int id)
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
            var item = Slots[slotType];
            if (item == null) return null;

            int id = item.Id;
            item.IsEquipped = false;
            Slots[slotType] = null;
            return id;
        }

        public void UnEquip(int id)
        {
            var item = FindItem(id);
            if (item == null) return;

            var slot = Slots[item.SlotType];
            if (slot != null && slot.Id == id)
            {
                slot.IsEquipped = false;
                Slots[item.SlotType] = null;
            }
            return;
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
                Slots[SlotType.Belt]?.ToJson(),
                Slots[SlotType.Feet]?.ToJson(),
                Slots[SlotType.Weapon]?.ToJson());
        }
    }
}
