using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Models.DTO;
using Server.Models.Skills.LearningMaster;
using Server.Repository;
using System.Net;

namespace Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LearningController : ControllerBase
    {
        private readonly UserRepository userRepository;
        private readonly UserStorage userStorage;
        protected APIResponse response;

        public LearningController(UserRepository _userRepository, UserStorage _userStorage)
        {
            userRepository = _userRepository;
            response = new();
            userStorage = _userStorage;
        }

        [HttpPost("get")]
        public async Task<IActionResult> GetSkills(NameRequestDTO dto)
        {
            var skillList = await userRepository.GetSkills(dto.Name);

            if (skillList == null)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.IsSuccess = false;
                return BadRequest(response);
            }

            response.StatusCode = HttpStatusCode.OK;
            response.IsSuccess = true;
            response.Result = skillList;
            return Ok(response);
        }

        [HttpPost("learn")]
        public async Task<IActionResult> LearnSkill(LearnSkillDTO dto)
        {
            var character = await userRepository.LearnSkill(dto);
            var activeCharacter = userStorage.ActiveUsers.FirstOrDefault(x => x.Character.CharacterName == dto.CharacterName);

            if (character == null || activeCharacter == null)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.IsSuccess = false;
                return BadRequest(response);
            }

            await activeCharacter.UpdateSpellList(character);

            response.StatusCode = HttpStatusCode.OK;
            response.IsSuccess = true;
            return Ok(response);
        }

        [HttpPost("forgot")]
        public async Task<IActionResult> ForgotSkill(LearnSkillDTO dto)
        {
            var character = await userRepository.ForgotSkill(dto);
            var activeCharacter = userStorage.ActiveUsers.FirstOrDefault(x => x.Character.CharacterName == dto.CharacterName);

            if (character == null || activeCharacter == null)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.IsSuccess = false;
                return BadRequest(response);
            }

            await activeCharacter.UpdateSpellList(character);

            response.StatusCode = HttpStatusCode.OK;
            response.IsSuccess = true;
            return Ok(response);
        }
    }
}
