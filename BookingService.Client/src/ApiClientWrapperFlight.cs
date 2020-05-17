using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BookingService.Client.Models;
using Newtonsoft.Json;

namespace BookingService.Client
{
    public partial class ApiClientWrapper
    {
        public async Task<List<Flight>> GetFlightsAsync()
        {
            var response = await Client.GetStringAsync($"{_url}/flights");
            var flights = JsonConvert.DeserializeObject<List<Flight>>(response);
            return flights;
        }

        public async Task<Flight> GetFlightByIdAsync(int id)
        {
            var response = await Client.GetAsync($"{_url}/flights/{id.ToString()}");
            if (!response.IsSuccessStatusCode)
                return null;
            var flight = JsonConvert.DeserializeObject<Flight>(await response.Content.ReadAsStringAsync());
            return flight;
        }

        public async Task<bool> CreateFlightAsync(Flight flight)
        {
            var response = await Client.PostAsync(
                $"{_url}/flights", 
                new StringContent(JsonConvert.SerializeObject(flight))
            );
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateFlightAsync(Flight newFlight)
        {
            var response = await Client.PutAsync(
                $"{_url}/flights",
                new StringContent(JsonConvert.SerializeObject(newFlight))
            );
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteFlightAsync(int id)
        {
            var response = await Client.DeleteAsync($"{_url}/flights/{id.ToString()}");
            return response.IsSuccessStatusCode;
        }
    }
}