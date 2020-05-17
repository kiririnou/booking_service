using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookingService.Client.Models;

namespace BookingService.Client
{
    public partial class ApiClientWrapper
    {
        public async Task<List<Flight>> GetFlightsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Flight> GetFlightByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CreateFlightAsync(Flight flight)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateFlightAsync(Flight newFlight)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteFlightAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}