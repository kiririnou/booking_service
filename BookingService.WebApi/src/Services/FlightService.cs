using System.Collections.Generic;
using System.Threading.Tasks;
using BookingService.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingService.WebApi.Services
{
    public class FlightService : IFlightService
    {
        private readonly dbContext _dbContext;

        public FlightService(dbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<List<Flight>> GetFlightsAsync()
        {
            return await _dbContext.Flights.ToListAsync();
        }

        public async Task<Flight> GetFlightByIdAsync(int id)
        {
            return await _dbContext.Flights.SingleOrDefaultAsync(f => f.Id == id);
        }

        public async Task<bool> CreateFlightAsync(Flight flight)
        {
            await _dbContext.Flights.AddAsync(flight);
            int created = await _dbContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<bool> UpdateFlightAsync(Flight newFlight)
        {
            _dbContext.Flights.Update(newFlight);
            int updated = await _dbContext.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> DeleteFlightAsync(int id)
        {
            var flight = await GetFlightByIdAsync(id);
            if (flight == null)
                return false;

            _dbContext.Flights.Remove(flight);
            int deleted = await _dbContext.SaveChangesAsync();
            return deleted > 0;
        }
    }
}