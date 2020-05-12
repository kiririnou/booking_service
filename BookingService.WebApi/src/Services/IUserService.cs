using System.Collections.Generic;
using System.Threading.Tasks;
using BookingService.WebApi.Models;

namespace BookingService.WebApi.Services
{
    public interface IUserService
    {
        Task<List<User>> GetUsersAsync();

        Task<User> GetUserByIdAsync(int id);

        Task<bool> CreateUserAsync(User user);

        Task<bool> UpdateUserAsync(User newUser);

        Task<bool> DeleteUserAsync(int id);
    }
}