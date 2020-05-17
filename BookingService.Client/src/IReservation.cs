using System.Collections.Generic;
using System.Threading.Tasks;
using BookingService.Client.Models;

namespace BookingService.Client
{
    public interface IReservation
    {
        Task<List<Reservation>> GetReservationsAsync();
        
        Task<Reservation> GetReservationByIdAsync(int id);
        
        Task<bool> CreateReservationAsync(Reservation reservation);
        
        Task<bool> UpdateReservationAsync(Reservation newReservation);
        
        Task<bool> DeleteReservationAsync(int id);
    }
}