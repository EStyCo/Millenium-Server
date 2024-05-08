﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Models.DTO;
using Server.Models.Utilities;
using Server.Repository;
using System.Net;

namespace Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StatsController : ControllerBase
    {
        private readonly UserStorage userStorage;
        private readonly IMapper mapper;

        public StatsController(UserStorage _userStorage, IMapper _mapper)
        {
            userStorage = _userStorage;
            mapper = _mapper;
        }

        [HttpPost("get")]
        public async Task<IActionResult> GetStats(NameRequestDTO dto)
        {
            Character? character = userStorage.ActiveUsers
                .Where(u => u.Character.Name == dto.Name)
                .Select(u => u.Character)
                .FirstOrDefault();

            if (character == null) return BadRequest(RespFactory.ReturnBadRequest());

            return Ok(RespFactory.ReturnOk(mapper.Map<StatDTO>(character)));
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateStats(UpdateStatDTO dto)
        {
            var character = await userStorage.UpdateStats(dto);

            if (character == null) return BadRequest(RespFactory.ReturnBadRequest());

            return Ok(RespFactory.ReturnOk());
        }
    }
}
