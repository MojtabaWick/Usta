using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Usta.Domain.Core.CityAgg.Contracts;
using Usta.Domain.Core.CityAgg.Dtos;
using Usta.Domain.Core.CityAgg.Entities;
using Usta.Infrastructure.EFCore.Persistence;

namespace Usta.Infrastructure.EFCore.Repositories.CityAgg
{
    public class CityRepository(AppDbContext dbContext) : ICityRepository
    {
        public async Task<bool> Add(City newCity, CancellationToken cancellationToken)
        {
            dbContext.Cities.Add(newCity);
            return await dbContext.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> Update(CityDto input, CancellationToken cancellationToken)
        {
            var affectedRow = await dbContext.Cities
                .Where(c => c.Id == input.Id)
                .ExecuteUpdateAsync(setter => setter
                    .SetProperty(c => c.Name, input.Name)
                    .SetProperty(c => c.UpdatedAt, DateTime.Now)
                    , cancellationToken);

            return affectedRow > 0;
        }

        public async Task<CityDto?> GetById(int id, CancellationToken cancellationToken)
        {
            return await dbContext.Cities
                .AsNoTracking()
                .Where(c => c.Id == id)
                .Select(c => new CityDto() { Id = c.Id, Name = c.Name })
                 .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<List<CityDto>> GetAllCities(CancellationToken cancellationToken)
        {
            return await dbContext.Cities
                .AsNoTracking()
                .Select(c => new CityDto()
                {
                    Id = c.Id,
                    Name = c.Name,
                }).ToListAsync(cancellationToken);
        }
    }
}