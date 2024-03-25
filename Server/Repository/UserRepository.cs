using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Server.Models;
using Server.Models.DTO;
using Server.Models.Skills;
using Server.Models.Skills.LearningMaster;
using Server.Models.Utilities;

namespace Server.Repository
{
    public class UserRepository
    {
        private readonly DbUserContext dbContext;
        private readonly IMapper mapper;

        public UserRepository(DbUserContext _dbContext, IMapper _mapper)
        {
            dbContext = _dbContext;
            mapper = _mapper;
        }

        public async Task<bool> IsUniqueUser(string email)
        {
            var result = await dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == email);

            if (result != null)
                return false;

            return true;
        }

        public async Task<LoginResponseDTO> LoginUser(string email, string password)
        {
            var user = await dbContext.Users
                .AsNoTracking()
                .Include(x => x.Character)
                .FirstOrDefaultAsync(x => x.Email == email.ToLower() && x.Password == password);

            if (user == null) return null;

            return new LoginResponseDTO
            {
                Character = mapper.Map<CharacterDTO>(user.Character)
            };
        }

        public async Task<bool> Registration(RegistrationRequestDTO regDTO)
        {
            if (regDTO != null)
            {
                var user = new User
                {
                    Email = regDTO.Email,
                    Password = regDTO.Password,
                };

                var character = new Character
                {
                    CharacterName = regDTO.CharacterName,
                    Gender = Gender.male,
                    Race = Race.Human,
                    CurrentArea = Place.Town,
                    User = user
                };
                user.Character = character;

                dbContext.Users.Add(user);
                dbContext.Characters.Add(character);

                await dbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<CharacterDTO> GetCharacter(string name)
        {
            var character = dbContext.Characters
                .FirstOrDefault(x => x.CharacterName == name);

            if (character == null) return null;

            return mapper.Map<CharacterDTO>(character);
        }

        public async Task<LearningResponseDTO> GetSkills(string name)
        {
            await Task.Delay(10);
            var character = dbContext.Characters
                .FirstOrDefault(x => x.CharacterName == name);

            if (character != null)
            {
                List<SkillType> skillTypeList = new() {character.Skill1,
                                                       character.Skill2,
                                                       character.Skill3,
                                                       character.Skill4,
                                                       character.Skill5 };

                return new LearningResponseDTO()
                {
                    FreePoints = character.FreePoints,
                    AllSkills = new SkillCollection().CreateLearningList(skillTypeList)
                };
            }
            return null;
        }

        public async Task<CharacterDTO> LearnSkill(LearnSkillDTO dto)
        {
            var character = dbContext.Characters
                .FirstOrDefault(x => x.CharacterName == dto.CharacterName);

            if (character == null)
                return null;

            var skillTypeList = new List<SkillType>
            {
                character.Skill1, character.Skill2, character.Skill3, character.Skill4, character.Skill5
            };

            if (!skillTypeList.Contains(SkillType.None) || skillTypeList.Contains(dto.SkillType))
                return null;

            var index = skillTypeList.FindIndex(x => x == SkillType.None);
            if (index != -1)
            {
                skillTypeList[index] = dto.SkillType;
            }
            else return null;

            character.Skill1 = skillTypeList[0];
            character.Skill2 = skillTypeList[1];
            character.Skill3 = skillTypeList[2];
            character.Skill4 = skillTypeList[3];
            character.Skill5 = skillTypeList[4];

            await dbContext.SaveChangesAsync();

            return mapper.Map<CharacterDTO>(character);
        }

        public async Task<CharacterDTO> ForgotSkill(LearnSkillDTO dto)
        {
            var character = dbContext.Characters
                .FirstOrDefault(x => x.CharacterName == dto.CharacterName);

            if (character == null)
                return null;

            var skillTypeList = new List<SkillType>
            {
                character.Skill1, character.Skill2, character.Skill3, character.Skill4, character.Skill5
            };

            var index = skillTypeList.FindIndex(x => x == dto.SkillType);
            if (index != -1)
            {
                skillTypeList[index] = SkillType.None;
            }
            else return null;

            character.Skill1 = skillTypeList[0];
            character.Skill2 = skillTypeList[1];
            character.Skill3 = skillTypeList[2];
            character.Skill4 = skillTypeList[3];
            character.Skill5 = skillTypeList[4];

            await dbContext.SaveChangesAsync();

            return mapper.Map<CharacterDTO>(character);
        }

        public async Task<CharacterDTO> UpdateStats(UpdateStatDTO dto)
        {
            var character = dbContext.Characters
                .FirstOrDefault(x => x.CharacterName == dto.Name);

            if (!StatValidator.CheckStats(character, dto)) return null;

            character = mapper.Map<Character>(dto);
            await dbContext.SaveChangesAsync();

            return mapper.Map<CharacterDTO>(character);
        }
    }
}
