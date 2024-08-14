using Microsoft.AspNetCore.Mvc;
using Server.Models.Utilities;
using Server.Models.Utilities.Slots;
using Server.Repository;
using Server.Services;

namespace Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AdminController(
        UserRepository userRep, 
        InventoryService invService) : ControllerBase
    {

        [HttpPost("UnEquipAllItems")]
        public async Task<IActionResult> UnEquipAllItems(string password)
        {
            await Task.Delay(0);
            if (password != "0001") return BadRequest(RespFactory.ReturnBadRequest("Не шали! По-моему ты не сюда зашел братан."));

            return Ok(RespFactory.ReturnOk());
        }

        [HttpPost("ClearItemsInventory")]
        public async Task<IActionResult> LoginUser(string password)
        {
            await Task.Delay(0);
            if (password != "0001") return BadRequest(RespFactory.ReturnBadRequest("Не шали! По-моему ты не сюда зашел братан."));

            return Ok(RespFactory.ReturnOk());
        }

        [HttpPost("AddItem")]
        public async Task<IActionResult> AddItem(string password, string name, ItemType typeItem)
        {
            await Task.Delay(0);
            if (password != "0001") 
                return BadRequest(RespFactory.ReturnBadRequest("Не шали! По-моему ты не сюда зашел братан."));
            if(!await userRep.CharacterExists(name) || !Enum.IsDefined(typeof(ItemType), typeItem))
                return BadRequest(RespFactory.ReturnBadRequest("Введены неверные данные. Повторите попытку."));

            await invService.AddItemsUser(name, typeItem);

            var item = ItemFactory.Get(typeItem);
            return Ok(RespFactory.ReturnOk($"{item?.Name} добавлен персонажу {name} в инвентарь."));
        }
    }
}
