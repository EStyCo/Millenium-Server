using Server.Hubs;
using Server.Models.DTO.Inventory;
using Server.Models.DTO.User;
using Server.Models.Inventory;
using Server.Models.Utilities;
using Server.Models.Utilities.Slots;
using Server.Repository;

namespace Server.Services
{
    public class InventoryService
    {
        private readonly UserStorage userStorage;
        private readonly ItemRepository itemRep;

        public InventoryService(UserStorage _userStorage,
                                ItemRepository _itemRep)
        {
            userStorage = _userStorage;
            itemRep = _itemRep;
        }

        public async Task<bool> EquipItem(DressingDTO dto)
        {
            if (!await itemRep.ItemExists(dto)) return false;
            var user = userStorage.GetUser(dto.Name);
            if (user == null) return false;

            var dressingItems = user.Inventory.Equip(dto.Id);

            if (dressingItems?.UnEquippedId != null)
            {
                await itemRep.UnEquipItem(new()
                {
                    Id = (int)dressingItems.UnEquippedId,
                    Name = dto.Name
                });
            }
            await itemRep.EquipItem(dto);
            user.ReAssembly();
            return true;
        }

        public async Task<bool> UnEquipItem(DressingDTO dto)
        {
            if (!await itemRep.ItemExists(dto)) return false;

            var user = userStorage.ActiveUsers.FirstOrDefault(x => x.Name == dto.Name);
            if (user == null) return false;

            user.Inventory.UnEquip(dto.Id);
            await itemRep.UnEquipItem(new()
            {
                Id = dto.Id,
                Name = dto.Name
            });
            user.ReAssembly();
            return true;
        }

        public async Task AddItemsUser(string name, params ItemType[] types)
        {
            var user = userStorage.GetUser(name);
            foreach (var type in types)
            {
                var item = await itemRep.AddItem(name, type);
                if (item != null)
                    user?.Inventory.AddItem(item);
            }
        }

        public async Task<bool> DestroyItem(DressingDTO dto)
        {
            var user = userStorage.GetUser(dto.Name);

            await itemRep.DestroyItem(dto);
            user?.Inventory.DeleteItem(dto.Id);
            user?.ReAssembly();
            return true;
        }

        /// <summary>
        /// Testing method
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<List<Tuple<string, Item?>>> GetInventoryFromDB(NameRequestDTO dto)
        {
            var listEF = await itemRep.GetInventory(dto.Name);
            var list = ItemFactory.GetList(listEF);
            var response = new List<Tuple<string, Item?>>();

            foreach (var item in list)
            {
                if (item.IsEquipped)
                {
                    response.Add(new(item.Name, item));
                }
            }
            return response;
        }
    }
}