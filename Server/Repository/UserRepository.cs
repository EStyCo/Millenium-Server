using Microsoft.EntityFrameworkCore;
using Server.Models;
using Server.Models.DTO;
using Server.Models.Utilities;

namespace Server.Repository
{
    public class UserRepository
    {
        private readonly DbUserContext dbContext;

        public UserRepository(DbUserContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task<bool> IsUniqueUser(string email, string characterName)
        {
            var result = await dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.CharacterName == characterName || x.Email == email);

            if (result != null)
                return false;

            return true;
        }

        public async Task<LoginResponseDTO> LoginUser(string email, string password)
        {
            var user = await dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == email.ToLower() && x.Password == password);

            ActivityUser activityUser = new();

            if (user != null)
            {
                activityUser.CharacterName = user.CharacterName;
                activityUser.Race = user.Race;
                activityUser.Level = user.Level;
                activityUser.CurrentArea = user.CurrentArea;
            }

            LoginResponseDTO loginResponseDTO = new()
            {
                User = activityUser
            };

            return loginResponseDTO;
        }

        public async Task<bool> Registration(RegistrationRequestDTO user)
        {
            if (user != null)
            {
                var newUser = new User
                {
                    CharacterName = user.CharacterName,
                    Email = user.Email,
                    Password = user.Password,
                    Race = user.Race,
                    Level = 1,
                    CurrentArea = Area.Town
                };

                dbContext.Users.Add(newUser);
                await dbContext.SaveChangesAsync();

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
