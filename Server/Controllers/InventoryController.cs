using Server.Models.DTO.Inventory;
using Microsoft.AspNetCore.Mvc;
using Server.Models.Inventory;
using Server.Models.Utilities;
using Server.Models.DTO.User;
using Server.Services;
using Server.Models.Inventory.Items;

namespace Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly UserStorage userStorage;

        private readonly InventoryService invService;

        public InventoryController(UserStorage _userStorage,
                                   InventoryService _invService)
        {
            userStorage = _userStorage;
            invService = _invService;
        }

        [HttpPost("getInventory")]
        public IActionResult GetInventory(NameRequestDTO dto)
        {
            var user = userStorage.ActiveUsers.FirstOrDefault(x => x.Name == dto.Name);
            if (user == null) return BadRequest(RespFactory.ReturnBadRequest());

            return Ok(RespFactory.ReturnOk(user.Inventory.ToJsonInventory()));
        }

        [HttpPost("getEquip")]
        public IActionResult GetEquip(NameRequestDTO dto)
        {
            var user = userStorage.ActiveUsers.FirstOrDefault(x => x.Name == dto.Name);
            if (user == null) return BadRequest(RespFactory.ReturnBadRequest());

            return Ok(RespFactory.ReturnOk(user.Inventory.ToJsonEquip()));
        }

        [HttpPost("getItem")]
        public IActionResult GetItem(DressingDTO dto)
        {
            return Ok(RespFactory.ReturnOk());
        }

        [HttpPut("equipItem")]
        public async Task<IActionResult> EquipItem(DressingDTO dto)
        {
            if (await invService.EquipItem(dto))
                return Ok(RespFactory.ReturnOk());
            return BadRequest(RespFactory.ReturnBadRequest());
        }


        [HttpPut("unEquipItem")]
        public async Task<IActionResult> UnEquipItem(DressingDTO dto)
        {
            if (await invService.UnEquipItem(dto))
                return Ok(RespFactory.ReturnOk());
            return BadRequest(RespFactory.ReturnBadRequest());
        }

        [HttpGet("destroyItem")]
        public IActionResult DestroyItem()
        {
            var user = userStorage.ActiveUsers.FirstOrDefault(x => x.Name == "Denny");

            if (user == null) return BadRequest(RespFactory.ReturnBadRequest());

            user.Inventory.AddItem(new Apple());
            return Ok(RespFactory.ReturnOk());
        }

        [HttpPost("getInventoryFromDB")]
        public async Task<IActionResult> GetInventoryFromDB(NameRequestDTO dto)
        {
            return Ok(await invService.GetInventoryFromDB(dto));
        }
    }
}
