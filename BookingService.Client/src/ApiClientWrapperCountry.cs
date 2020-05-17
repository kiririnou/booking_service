using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookingService.Client.Models;

namespace BookingService.Client
{
    public partial class ApiClientWrapper
    {
        public async Task<List<Country>> GetCountriesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Country> GetCountryByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CreateCountryAsync(Country country)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateCountryAsync(Country newCountry)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteCountryAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}