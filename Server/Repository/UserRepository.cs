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
            var result = await dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == dto.Email || x.Character.Name == dto.Name);

            if (result != null)
                return false;

            return true;
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

            var stats = new Stats
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
                FreePoints = character.FreelSpellPoints,
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
                || character.FreelSpellPoints <= 0) return null;

            character.Spells.Add(dto.SpellType);
            character.FreelSpellPoints--;
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
            character.FreelSpellPoints++;
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

        public async Task<Stats> GetStats(string name)
        {
#pragma warning disable CS8603 
            return await dbContext.Characters
                .Where(x => x.Name == name)
                .Select(x => x.Stats)
                .AsNoTracking()
                .FirstOrDefaultAsync() ?? null;
#pragma warning restore CS8603 
        }

        public async Task<List<Item>> GetInventory(string name)
        {
            var items = new List<Item>();
            var itemsEF = await dbContext.Characters
                           .AsNoTracking()
                           .Where(x => x.Name == name)
                           .Select(x => x.Items)
                           .FirstOrDefaultAsync();
            if (itemsEF != null)
            {
                var a = ItemFactory.GetList(itemsEF);
                items.AddRange(a);
            }
            return items;
        }

        public async Task<bool> UserExists(string name)
        {
            var character = await dbContext.Characters
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Name == name);

            return character != null;
        }

        public async Task<bool> ItemExists(DressingDTO dto)
        {
            var item = await dbContext.Characters
                .AsNoTracking()
                .Where(x => x.Name == dto.Name)
                .SelectMany(x => x.Items)
                .FirstOrDefaultAsync(x => x.Id == dto.Id);

            return item != null;
        }

        public async Task EquipItem(DressingDTO dto)
        {
            var item = await dbContext.Characters
                .Where(x => x.Name == dto.Name)
                .SelectMany(x => x.Items)
                .FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (item != null)
            {
                item.IsEquipped = true;
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task UnEquipItem(DressingDTO dto)
        {
            var item = await dbContext.Characters
                .Where(x => x.Name == dto.Name)
                .SelectMany(x => x.Items)
                .FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (item != null)
            {
                item.IsEquipped = false;
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
