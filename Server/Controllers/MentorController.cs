using Microsoft.AspNetCore.Mvc;
using Server.Models.DTO.User;
using Server.Models.Skills;
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
        public IActionResult GetSkills(NameRequestDTO dto)
        {
            var skillList = userRepository.GetSkills(dto.Name);

            if (skillList == null)
            {
                return BadRequest(RespFactory.ReturnBadRequest());
            }

            return Ok(RespFactory.ReturnOk(skillList));
        }

        [HttpPost("learn")]
        public async Task<IActionResult> LearnSkill(SpellRequestDTO dto)
        {
            var spellList = await userRepository.LearnSkill(dto);
            var user = storage.ActiveUsers.FirstOrDefault(x => x.Name == dto.Name);

            if (user != null)
                user.ActiveSkills = spellList;
            return Ok(RespFactory.ReturnOk());
        }

        [HttpPost("forgot")]
        public async Task<IActionResult> ForgotSkill(SpellRequestDTO dto)
        {
            var spellList = await userRepository.ForgotSkill(dto);
            var user = storage.ActiveUsers.FirstOrDefault(x => x.Name == dto.Name);

            if (user != null)
                user.ActiveSkills = spellList;
            return Ok(RespFactory.ReturnOk());
        }
    }
}
