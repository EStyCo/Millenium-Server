using Server.Models.DTO.Inventory;
using Server.Models.DTO.User;
using Server.Models.Inventory;
using Server.Repository;

namespace Server.Services
{
    public class InventoryService
    {
        private readonly UserStorage userStorage;
        private readonly UserRepository userRep;

        public InventoryService(UserStorage _userStorage,
                                UserRepository _userRepository)
        {
            userStorage = _userStorage;
            userRep = _userRepository;
        }

        public async Task<bool> EquipItem(DressingDTO dto)
        {
            if (!await userRep.ItemExists(dto)) return false;

            var user = userStorage.ActiveUsers.FirstOrDefault(x => x.Name == dto.Name);
            if (user == null) return false;

            var result = user.Inventory.Equip(dto.Id);

            if (result != null)
            {
                await userRep.EquipItem(dto);
                if (result.UnEquippedId != null)
                {
                    await userRep.UnEquipItem(new()
                    {
                        Id = (int)result.UnEquippedId,
                        Name = dto.Name
                    });
                }
            }
            return true;
        }

        public async Task<bool> UnEquipItem(DressingDTO dto)
        {
            if (!await userRep.ItemExists(dto)) return false;

            var user = userStorage.ActiveUsers.FirstOrDefault(x => x.Name == dto.Name);
            if (user == null) return false;

            var id = user.Inventory.UnEquip(dto.Id);

            await userRep.UnEquipItem(new()
            {
                Id = dto.Id,
                Name = dto.Name
            });
            return true;
        }


        /// <summary>
        /// Testing method
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<List<Tuple<string, Item?>>> GetInventoryFromDB(NameRequestDTO dto)
        {
            var list = await userRep.GetInventory(dto.Name);
            var response = new List<Tuple<string, Item?>>();

            foreach (var item in list)
            {
                if(item.IsEquipped)
                {
                    response.Add(new(item.Name, item));
                }
            }

            return response;
        }
    }
}