using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Models.DTO;
using Server.Models.Utilities;
using Server.Models.Skills;

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
            var list = storage.ActiveUsers
                .Where(x => x.Name == dto.Name)
                .Select(x => x.ActiveSkills)
                .First();                

            return Ok(RespFactory.ReturnOk(new CustomList<Spell>(list)));
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
