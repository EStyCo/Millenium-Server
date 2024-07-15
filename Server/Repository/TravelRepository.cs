using Microsoft.EntityFrameworkCore;
using Server.EntityFramework;
using Server.Models.DTO.User;

namespace Server.Repository
{
    public class TravelRepository
    {
        private readonly DbUserContext dbContext;

        public TravelRepository(DbUserContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task<TravelDTO> GetArea(NameRequestDTO dto)
        {
            var user = await dbContext.Characters
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Name == dto.Name);

            if (user != null)
            {
                return new TravelDTO(user.Name, user.Place);
            }

            return null;
        }

        public async Task<TravelDTO> GoNewArea(TravelDTO dto)
        {
            var user = await dbContext.Characters
                .FirstOrDefaultAsync(x => x.Name == dto.Name);

            if (user == null) return null;

            user.Place = dto.Place;
            await dbContext.SaveChangesAsync();

            return new TravelDTO(user.Name, user.Place);
        }
    }
}
