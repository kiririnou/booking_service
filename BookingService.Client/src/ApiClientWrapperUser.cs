using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BookingService.Client.Models;
using Newtonsoft.Json;

namespace BookingService.Client
{
    public partial class ApiClientWrapper
    {
        public async Task<List<User>> GetUsersAsync()
        {
            var response = await Client.GetStringAsync($"{_url}/users");
            var users = JsonConvert.DeserializeObject<List<User>>(response);
            return users;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var response = await Client.GetAsync($"{_url}/users/{id.ToString()}");
            if (!response.IsSuccessStatusCode)
                return null;
            var user = JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());
            return user;
        }

        public async Task<bool> CreateUserAsync(User user)
        {
            var response = await Client.PostAsync(
                $"{_url}/users", 
                new StringContent(JsonConvert.SerializeObject(user))
            );
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateUserAsync(User newUser)
        {
            var response = await Client.PutAsync(
                $"{_url}/users",
                new StringContent(JsonConvert.SerializeObject(newUser))
            );
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var response = await Client.DeleteAsync($"{_url}/users/{id.ToString()}");
            return response.IsSuccessStatusCode;
        }
    }
}