using System.Collections.Generic;
using System.Threading.Tasks;
using BookingService.Client.Models;

namespace BookingService.Client
{
    public interface ICountry
    {
        Task<List<Country>> GetCountriesAsync();

        Task<Country> GetCountryByIdAsync(int id);

        Task<bool> CreateCountryAsync(Country country);

        Task<bool> UpdateCountryAsync(Country newCountry);

        Task<bool> DeleteCountryAsync(int id);
    }
}