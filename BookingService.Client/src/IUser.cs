using System.Collections.Generic;
using System.Threading.Tasks;
using BookingService.Client.Models;

namespace BookingService.Client
{
    public interface IUser
    {
        Task<List<User>> GetUsersAsync();

        Task<User> GetUserByIdAsync(int id);

        Task<bool> CreateUserAsync(User user);

        Task<bool> UpdateUserAsync(User newUser);

        Task<bool> DeleteUserAsync(int id);
    }
}