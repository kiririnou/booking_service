using System.Collections.Generic;
using System.Threading.Tasks;
using BookingService.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingService.WebApi.Services
{
    public class UserService : IUserService
    {
        private readonly dbContext _dbContext;

        public UserService(dbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(u => u.Id == id);
        }

        public async Task<bool> CreateUserAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            int created = await _dbContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<bool> UpdateUserAsync(User newUser)
        {
            _dbContext.Users.Update(newUser);
            var updated = await _dbContext.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await GetUserByIdAsync(id);
            if (user == null)
                return false;
            
            _dbContext.Users.Remove(user);
            int deleted = await _dbContext.SaveChangesAsync();
            return deleted > 0;
        }
    }
}