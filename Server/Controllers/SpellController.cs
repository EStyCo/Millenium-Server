﻿using Microsoft.AspNetCore.Mvc;
using Server.Models.Utilities;
using Server.Models.Spells;
using Server.Models.DTO.User;
using Server.Hubs;
using Server.Models.DTO;

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

        [HttpPost("list")]
        public async Task<IActionResult> GetListSpells(NameRequestDTO dto)
        {
            var user = storage.GetUser(dto.Name);
            if (user == null) return BadRequest(RespFactory.ReturnBadRequest());
            return Ok(RespFactory.ReturnOk(new SpellListResponse(user)));
        }
    }
}
