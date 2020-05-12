using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingService.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingService.WebApi.Services
{
    public class CountryService : ICountryService
    {
        private readonly dbContext _dbContext;

        public CountryService(dbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Country>> GetCountriesAsync()
        {
            return await _dbContext.Countries.ToListAsync();
        }

        public async Task<Country> GetCountryByIdAsync(int id)
        {
            return await _dbContext.Countries.SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> CreateCountryAsync(Country country)
        {
            await _dbContext.Countries.AddAsync(country);
            int created = await _dbContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<bool> UpdateCountryAsync(Country newCountry)
        {
            _dbContext.Countries.Update(newCountry);
            var updated = await _dbContext.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> DeleteCountryAsync(int id)
        {
            var country = await GetCountryByIdAsync(id);

            if (country == null)
                return false;

            _dbContext.Countries.Remove(country);
            var deleted = await _dbContext.SaveChangesAsync();
            return deleted > 0;
        }
    }
}