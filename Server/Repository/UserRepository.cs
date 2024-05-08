using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Server.Models;
using Server.Models.DTO;
using Server.Models.DTO.Auth;
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

        public async Task<bool> Registration(RegRequestDTO regDTO)
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
                    Name = regDTO.Name,
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

        public async Task<Character> GetCharacter(string name)
        {
            return await dbContext.Characters.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<MentorSpellListResponseDTO> GetSkills(string name)
        {
            await Task.Delay(10);
            var character = dbContext.Characters
                .FirstOrDefault(x => x.Name == name);

            if (character != null)
            {
                List<SpellType> spellTypeList = new() {character.Spell1,
                                                       character.Spell2,
                                                       character.Spell3,
                                                       character.Spell4,
                                                       character.Spell5 };

                return new MentorSpellListResponseDTO()
                {
                    FreePoints = character.FreePoints,
                    SpellList = new SkillCollection().CreateLearningList(spellTypeList)
                };
            }
            return null;
        }

        public async Task<Character> LearnSkill(SpellRequestDTO dto)
        {
            var character = dbContext.Characters
                .FirstOrDefault(x => x.Name == dto.Name);

            if (character == null)
                return null;

            var skillTypeList = new List<SpellType>
            {
                character.Spell1, character.Spell2, character.Spell3, character.Spell4, character.Spell5
            };

            if (!skillTypeList.Contains(SpellType.None) || skillTypeList.Contains(dto.SpellType) || character.FreelSpellPoints <= 0)
                return null;

            var index = skillTypeList.FindIndex(x => x == SpellType.None);
            if (index != -1)
            {
                skillTypeList[index] = dto.SpellType;
                character.FreePoints--;
            }
            else return null;

            character.Spell1 = skillTypeList[0];
            character.Spell2 = skillTypeList[1];
            character.Spell3 = skillTypeList[2];
            character.Spell4 = skillTypeList[3];
            character.Spell5 = skillTypeList[4];

            await dbContext.SaveChangesAsync();

            return character;
        }

        public async Task<Character> ForgotSkill(SpellRequestDTO dto)
        {
            var character = dbContext.Characters
                .FirstOrDefault(x => x.Name == dto.Name);

            if (character == null)
                return null;

            var skillTypeList = new List<SpellType>
            {
                character.Spell1, character.Spell2, character.Spell3, character.Spell4, character.Spell5
            };

            var index = skillTypeList.FindIndex(x => x == dto.SpellType);
            if (index != -1)
            {
                skillTypeList[index] = SpellType.None;
                character.FreePoints++;
            }
            else return null;

            character.Spell1 = skillTypeList[0];
            character.Spell2 = skillTypeList[1];
            character.Spell3 = skillTypeList[2];
            character.Spell4 = skillTypeList[3];
            character.Spell5 = skillTypeList[4];

            await dbContext.SaveChangesAsync();

            return character;
        }

        public async Task<Character> UpdateStats(UpdateStatDTO dto)
        {
            var character = await dbContext.Characters
                .FirstOrDefaultAsync(x => x.Name == dto.Name);

            if (!StatValidator.CheckStats(character, dto)) return null;

            character.Strength = dto.Strength;
            character.Agility = dto.Agility;
            character.Intelligence = dto.Intelligence;
            character.FreePoints = dto.FreePoints;

            //character = mapper.Map<Character>(dto);
            await dbContext.SaveChangesAsync();

            return character;
        }

        public async Task<bool> UserExists(string name)
        {
            var character = await dbContext.Characters
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Name == name);

            return character != null;
        }
    }
}
