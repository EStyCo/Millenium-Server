﻿using Microsoft.EntityFrameworkCore;
using Server.Models;
using Server.Models.DTO;

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
                return new TravelDTO
                {
                    Name = user.Name,
                    Place = user.CurrentArea
                };
            }

            return null;
        }

        public async Task<TravelDTO> GoNewArea(TravelDTO dto)
        {
            var user = await dbContext.Characters
                .FirstOrDefaultAsync(x => x.Name == dto.Name);

            if (user != null)
            {
                user.CurrentArea = dto.Place;
                await dbContext.SaveChangesAsync();

                return new TravelDTO
                {
                    Name = user.Name,
                    Place = user.CurrentArea
                };
            }

            return null;
        }
    }
}
