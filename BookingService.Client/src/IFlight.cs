using System.Collections.Generic;
using System.Threading.Tasks;
using BookingService.Client.Models;

namespace BookingService.Client
{
    public interface IFlight
    {
        Task<List<Flight>> GetFlightsAsync();

        Task<Flight> GetFlightByIdAsync(int id);

        Task<bool> CreateFlightAsync(Flight flight);

        Task<bool> UpdateFlightAsync(Flight newFlight);

        Task<bool> DeleteFlightAsync(int id);
    }
}