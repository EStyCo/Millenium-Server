using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Models.Utilities;
using Server.Models.Spells;
using Server.Models.DTO.User;

namespace Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SpellController : ControllerBase
    {
        private readonly UserStorage storage;
        protected APIResponse response = new();

        public SpellController(UserStorage _storage)
        {
            storage = _storage;
        }

        [HttpPost("getListSpells")]
        public async Task<IActionResult> GetListSpells(NameRequestDTO dto)
        {
            var user = storage.ActiveUsers.FirstOrDefault(x => x.Name == dto.Name);
            if (user == null) return BadRequest(RespFactory.ReturnBadRequest());

            var response = new SpellListResponse(user.CanAttack, user.GlobalRestSeconds, user.ActiveSkills); 

            return Ok(RespFactory.ReturnOk(response));
        }

        /*[HttpPost("getSpell")]
        public async Task<IActionResult> GetSpell(NameRequestDTO dto)
        {
            var user = userStorage.ActiveUsers.FirstOrDefault(x => x.Character.Name == dto.Name);

            if (user == null)
            {
                return BadRequest(RespFactory.ReturnBadRequest());
            }

            return Ok(RespFactory.ReturnOk(new CustomList<Spell>(user.ActiveSkills)));
        }*/
    }
}
