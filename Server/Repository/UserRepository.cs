using Server.Models.Skills.LearningMaster;
using Server.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using Server.Models.DTO.Inventory;
using Server.Models.Utilities;
using Server.Models.Inventory;
using Server.EntityFramework;
using Server.Models.DTO.Auth;
using Server.Models.DTO.User;
using Server.Models.Spells;
using AutoMapper;

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

        public async Task<bool> IsUniqueUser(RegRequestDTO dto)
        {
            return await dbContext.Users
                .AsNoTracking()
                .AnyAsync(x => x.Email == dto.Email || x.Character.Name == dto.Name);
        }

        public async Task<CharacterEF?> LoginUser(LoginRequestDTO dto)
        {
            var character = await dbContext.Characters
                .AsNoTracking()
                .Where(x => x.User.Email == dto.Email.ToLower() && x.User.Password == dto.Password)
                .Include(x => x.Items)
                .Include(x => x.Stats)
                .FirstOrDefaultAsync();
            if (character == null) return null;
            return character;
        }

        public async Task<bool> Registration(RegRequestDTO regDTO)
        {
            if (regDTO == null) return false;

            var user = new UserEF
            {
                Email = regDTO.Email,
                Password = regDTO.Password,
            };

            var character = new CharacterEF
            {
                Name = regDTO.Name,
                Gender = Gender.male,
                Race = Race.Human,
                Place = "town",
                User = user,
                Spells = [],
                Items = []
            };

            var stats = new StatsEF
            {
                CharacterEF = character
            };

            user.Character = character;
            character.Stats = stats;

            dbContext.Users.Add(user);
            dbContext.Characters.Add(character);

            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<CharacterEF?> GetCharacter(string name)
        {
            return await dbContext.Characters.FirstOrDefaultAsync(x => x.Name == name);
        }

        public MentorSpellListResponseDTO GetSkills(string name)
        {
            var character = dbContext.Characters.FirstOrDefault(x => x.Name == name);
            if (character == null) return null;

            return new MentorSpellListResponseDTO()
            {
                FreePoints = character.FreeSpellPoints,
                TotalPoints = character.TotalSpellPoints,
                SpellList = SpellFactory.GetLearningList(character.Spells)
            };
        }

        public async Task<List<Spell>> LearnSkill(SpellRequestDTO dto)
        {
            var character = dbContext.Characters
                .FirstOrDefault(x => x.Name == dto.Name);

            if (character == null) return null;
            if (character.Spells.Contains(dto.SpellType)
                || character.FreeSpellPoints <= 0) return null;

            character.Spells.Add(dto.SpellType);
            character.FreeSpellPoints--;
            await dbContext.SaveChangesAsync();
            return SpellFactory.Get(character.Spells);
        }

        public async Task<List<Spell>> ForgotSkill(SpellRequestDTO dto)
        {
            var character = dbContext.Characters
                .FirstOrDefault(x => x.Name == dto.Name);

            if (character == null) return null;

            if (character.Spells.Contains(dto.SpellType))
            {
                character.Spells.Remove(dto.SpellType);
            }
            character.FreeSpellPoints++;
            await dbContext.SaveChangesAsync();
            return SpellFactory.Get(character.Spells);
        }

        public async Task UpdateStats(UpdateStatDTO dto)
        {
            var stats = await dbContext.Characters
                .Where(c => c.Name == dto.Name)
                .Select(c => c.Stats)
                .FirstOrDefaultAsync();

            if (stats?.ChangeStats(dto) ?? false)
            {
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task UpdateExp(int currentExp, string name)
        {
            var stats = await dbContext.Characters
                .Where(c => c.Name == name)
                .Select(c => c.Stats)
                .FirstOrDefaultAsync();

            stats?.ChangeExp(currentExp);
            await dbContext.SaveChangesAsync();
        }

        public async Task<StatsEF?> GetStats(string name)
        {
            return await dbContext.Characters
                .Where(x => x.Name == name)
                .Select(x => x.Stats)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<bool> CharacterExists(string name)
        {
            return await dbContext.Characters
                .AsNoTracking()
                .AnyAsync(x => x.Name == name);
        }
    }
}
