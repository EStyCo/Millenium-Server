using Server.EntityFramework.Models;
using Server.Models.Inventory;
using Server.Models.Inventory.Items;
using Server.Models.Inventory.Items.Body;
using Server.Models.Inventory.Items.Feet;
using Server.Models.Inventory.Items.Head;
using Server.Models.Inventory.Items.Weapon;
using Server.Models.Utilities.Slots;

namespace Server.Models.Utilities
{
    public static class ItemFactory
    {
        private static Dictionary<ItemType, Func<Item>> itemDictionary = new()
        {
            { ItemType.Apple, () => new Apple() },
            { ItemType.Spacesuit, () => new Spacesuit() },
            { ItemType.TitanArmor, () => new TitanArmor() },
            { ItemType.TitanLance, () => null },
            { ItemType.TitanSword, () => new TitanSword() },
            { ItemType.KnightHelmet, () => null },
            { ItemType.TitanHelmet, () => new TitanHelmet() },
            { ItemType.ChainBoots, () => new ChainBoots() },
            { ItemType.LeatherArmor, () => new LeatherArmor() },
            { ItemType.Stone, () => new Stone() },
            { ItemType.Bloodletter, () => new Bloodletter() },
        };

        public static Item? Get(ItemEF dto)
        {
            if (itemDictionary.TryGetValue(dto.Type, out var factory))
            {
                var item = factory?.Invoke();
                if (item == null) return null;

                item.Id = dto.Id;
                item.IsEquipped = dto.IsEquipped;
                return item;
            }
            return null;
        }

        public static Item? Get(ItemType type)
        {
            itemDictionary.TryGetValue(type, out var factory);
            return factory?.Invoke();
        }

        public static List<Item> GetList(List<ItemEF> listItemsEF)
        {
            var listItems = new List<Item>();
            if (listItemsEF == null) return listItems;

            foreach (var itemEF in listItemsEF)
            {
                var item = Get(itemEF);
                if (item != null)
                    listItems.Add(item);
            }
            return listItems;
        }

        public static List<Item> GetList(List<ItemType> listTypes)
        {
            var listItems = new List<Item>();
            if (listTypes == null || listTypes.Count <= 0) return listItems;

            foreach (var itemType in listTypes)
            {
                var item = Get(itemType);
                if (item != null)
                    listItems.Add(item);
            }
            return listItems;
        }
    }
}
