using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Models.Locations;
using Server.Models;
using Server.Models.DTO;
using System.Net;
using Server.Models.Utilities;
using Server.Models.Skills;

namespace Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SpellController : ControllerBase
    {
        private readonly UserStorage userStorage;
        protected APIResponse response = new();

        public SpellController(UserStorage _userStorage)
        {
            userStorage = _userStorage;
        }

        [HttpPost("getListSpells")]
        public async Task<IActionResult> GetListSpells(NameRequestDTO dto)
        {
            var user = userStorage.ActiveUsers.FirstOrDefault(x => x.Character.Name == dto.Name);

            if (user == null) 
            {
                return BadRequest(RespFactory.ReturnBadRequest());
            }

            return Ok(RespFactory.ReturnOk(new CustomList<Spell>(user.ActiveSkills)));
        }

        [HttpPost("getSpell")]
        public async Task<IActionResult> GetSpell(NameRequestDTO dto)
        {
            var user = userStorage.ActiveUsers.FirstOrDefault(x => x.Character.Name == dto.Name);

            if (user == null)
            {
                return BadRequest(RespFactory.ReturnBadRequest());
            }

            return Ok(RespFactory.ReturnOk(new CustomList<Spell>(user.ActiveSkills)));
        }
    }
}
