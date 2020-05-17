using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookingService.Client.Models;

namespace BookingService.Client
{
    public partial class ApiClientWrapper
    {
        public async Task<List<Reservation>> GetReservationsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Reservation> GetReservationByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
        
        public async Task<bool> CreateReservationAsync(Reservation reservation)
        {
            throw new NotImplementedException();
        }
        
        public async Task<bool> UpdateReservationAsync(Reservation newReservation)
        {
            throw new NotImplementedException();
        }
        
        public async Task<bool> DeleteReservationAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}