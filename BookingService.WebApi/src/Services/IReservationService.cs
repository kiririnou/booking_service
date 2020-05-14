using System.Collections.Generic;
using System.Threading.Tasks;
using BookingService.WebApi.Models;

namespace BookingService.WebApi.Services
{
    public interface IReservationService
    {
        Task<List<Reservation>> GetReservationsAsync();

        Task<Reservation> GetReservationByIdAsync(int id);

        Task<bool> CreateReservationAsync(Reservation reservation);

        Task<bool> UpdateReservationAsync(Reservation newReservation);

        Task<bool> DeleteReservationAsync(int id);
    }
}