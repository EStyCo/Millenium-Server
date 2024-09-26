using Server.Models.DTO.Inventory;
using Microsoft.AspNetCore.Mvc;
using Server.Models.Utilities;
using Server.Models.DTO.User;
using Server.Services;
using Server.Models.Inventory.Items;
using Server.Hubs;

namespace Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly UserStorage userStorage;
        private readonly InventoryService invService;

        public InventoryController(
            UserStorage _userStorage,
            InventoryService _invService)
        {
            userStorage = _userStorage;
            invService = _invService;
        }

        [HttpPost("Inventory")]
        public IActionResult GetInventory(NameRequestDTO dto)
        {
            var user = userStorage.GetUser(dto.Name);
            if (user == null) return BadRequest(RespFactory.ReturnBadRequest());
            return Ok(RespFactory.ReturnOk(user.Inventory.ToJsonInventory()));
        }

        [HttpPost("GetEquip")]
        public IActionResult GetEquip(NameRequestDTO dto)
        {
            var user = userStorage.GetUser(dto.Name);
            if (user == null) return BadRequest(RespFactory.ReturnBadRequest());
            return Ok(RespFactory.ReturnOk(user.Inventory.ToJsonEquip()));
        }

        [HttpPut("Equip")]
        public async Task<IActionResult> EquipItem(DressingDTO dto)
        {
            if (await invService.EquipItem(dto))
                return Ok(RespFactory.ReturnOk());
            return BadRequest(RespFactory.ReturnBadRequest());
        }


        [HttpPut("UnEquip")]
        public async Task<IActionResult> UnEquipItem(DressingDTO dto)
        {
            if (await invService.UnEquipItem(dto))
                return Ok(RespFactory.ReturnOk());
            return BadRequest(RespFactory.ReturnBadRequest());
        }

        [HttpPost("Destroy")]
        public async Task<IActionResult> DestroyItem(DressingDTO dto)
        {
            if (await invService.DestroyItem(dto))
                return Ok(RespFactory.ReturnOk());
            return BadRequest(RespFactory.ReturnBadRequest());
        }

        [HttpPost("getInventoryFromDB")]
        public async Task<IActionResult> GetInventoryFromDB(NameRequestDTO dto)
        {
            return Ok(await invService.GetInventoryFromDB(dto));
        }
    }
}
