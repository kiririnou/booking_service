using System.Collections.Generic;
using System.Threading.Tasks;
using BookingService.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingService.WebApi.Services
{
    public class ReservationService : IReservationService
    {
        private readonly dbContext _dbContext;

        public ReservationService(dbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Reservation>> GetReservationsAsync()
        {
            return await _dbContext.Reservations.ToListAsync();
        }

        public async Task<Reservation> GetReservationByIdAsync(int id)
        {
            return await _dbContext.Reservations.SingleOrDefaultAsync(u => u.Id == id);
        }

        public async Task<bool> CreateReservationAsync(Reservation reservation)
        {
            await _dbContext.Reservations.AddAsync(reservation);
            int created = await _dbContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<bool> UpdateReservationAsync(Reservation newReservation)
        {
            _dbContext.Reservations.Update(newReservation);
            var updated = await _dbContext.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> DeleteReservationAsync(int id)
        {
            var reservation = await GetReservationByIdAsync(id);
            if (reservation == null)
                return false;
            
            _dbContext.Reservations.Remove(reservation);
            int deleted = await _dbContext.SaveChangesAsync();
            return deleted > 0;
        }
    }
}