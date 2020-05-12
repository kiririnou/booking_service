using System.Collections.Generic;
using System.Threading.Tasks;
using BookingService.WebApi.Models;

namespace BookingService.WebApi.Services
{
    public interface IFlightService
    {
        Task<List<Flight>> GetFlightsAsync();

        Task<Flight> GetFlightByIdAsync(int id);

        Task<bool> CreateFlightAsync(Flight flight);

        Task<bool> UpdateFlightAsync(Flight newFlight);

        Task<bool> DeleteFlightAsync(int id);
    }
}