﻿using Microsoft.AspNetCore.Mvc;
using Server;
using Server.Models;
using Server.Models.DTO.Auth;
using Server.Models.DTO.User;
using Server.Models.Utilities;
using Server.Repository;
using System.Net;

namespace WebApplication1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly UserStorage storage;

        public LogsController(UserStorage _userStorage)
        {
            storage = _userStorage;
        }

        [HttpPost("get")]
        public async Task<IActionResult> GetBattleLogs(NameRequestDTO dto)
        {
            var user = storage.ActiveUsers.FirstOrDefault(x => x.Name == dto.Name);

            if (user != null)
            {
                return Ok(RespFactory.ReturnOk(new CustomList<string>(user.BattleLogs)));
            }
            return BadRequest(RespFactory.ReturnBadRequest(new CustomList<string>(new List<string>())));
        }
    }
}

