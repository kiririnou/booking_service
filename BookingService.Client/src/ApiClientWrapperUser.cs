using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookingService.Client.Models;

namespace BookingService.Client
{
    public partial class ApiClientWrapper
    {
        public async Task<List<User>> GetUsersAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CreateUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateUserAsync(User newUser)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}