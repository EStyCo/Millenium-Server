using Microsoft.EntityFrameworkCore;
using Server.Models;
using Server.Models.DTO;
using Server.Models.Locations;
using Server.Models.Utilities;
using Area = Server.Models.Utilities.Area;

namespace Server.Repository
{
    public class UserRepository
    {
        private readonly DbUserContext dbContext;

        public UserRepository(DbUserContext _dbContext)
        {
            dbContext = _dbContext;
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

            CharacterDTO character = new();
            LoginResponseDTO loginResponseDTO = new();

            if (user != null)
            {
                character.CharacterName = user.Character.CharacterName;
                character.Race = user.Character.Race;
                character.Gender = user.Character.Gender;
                character.Level = user.Character.Level;
                character.Exp = user.Character.Exp;
                character.TotalPoints = user.Character.TotalPoints;
                character.FreePoints = user.Character.FreePoints;
                character.Strength = user.Character.Strength;
                character.Agility = user.Character.Agility;
                character.Intelligence = user.Character.Intelligence;

                loginResponseDTO.Character = character ;
                return loginResponseDTO;
            }

            return loginResponseDTO;
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
                    CurrentArea = Area.Town,
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

            if (character != null)
            {
                return new CharacterDTO()
                {
                    CharacterName = character.CharacterName,
                    Race = character.Race,
                    Gender = character.Gender,
                    CurrentArea = character.CurrentArea,
                    Level = character.Level,
                    Exp = character.Exp,
                    TotalPoints = character.TotalPoints,
                    FreePoints = character.FreePoints,
                    Strength = character.Strength,
                    Agility = character.Agility,
                    Intelligence = character.Intelligence,
                };
            }
            return null;
        }
    }
}
