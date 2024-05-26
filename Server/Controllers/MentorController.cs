using Microsoft.AspNetCore.Mvc;
using Server.Models.DTO;
using Server.Models.Skills.LearningMaster;
using Server.Models.Utilities;
using Server.Repository;

namespace Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MentorController : ControllerBase
    {
        private readonly UserRepository userRepository;
        private readonly UserStorage storage;

        public MentorController(UserRepository _userRepository, UserStorage _storage)
        {
            userRepository = _userRepository;
            storage = _storage;
        }

        [HttpPost("get")]
        public async Task<IActionResult> GetSkills(NameRequestDTO dto)
        {
            var skillList = await userRepository.GetSkills(dto.Name);

            if (skillList == null)
            {
                return BadRequest(RespFactory.ReturnBadRequest());
            }

            return Ok(RespFactory.ReturnOk(skillList));
        }

        [HttpPost("learn")]
        public async Task<IActionResult> LearnSkill(SpellRequestDTO dto)
        {
            var character = await userRepository.LearnSkill(dto);
            var user = storage.ActiveUsers.FirstOrDefault(x => x.Name == dto.Name);

            if (character != null && user != null)
            {
                user.CreateSpellList(character);
            }

            return Ok(RespFactory.ReturnOk());
        }

        [HttpPost("forgot")]
        public async Task<IActionResult> ForgotSkill(SpellRequestDTO dto)
        {
            var character = await userRepository.ForgotSkill(dto);
            var activeCharacter = storage.ActiveUsers.FirstOrDefault(x => x.Name == dto.Name);

            if (character == null || activeCharacter == null)
            {
                return BadRequest(RespFactory.ReturnBadRequest());
            }

            return Ok(RespFactory.ReturnOk());
        }
    }
}
