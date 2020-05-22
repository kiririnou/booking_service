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
            var response = await Client.GetAsync($"{_url}/flights");
            if (!response.IsSuccessStatusCode)
                return null;
            var flights = JsonConvert.DeserializeObject<List<Flight>>(await response.Content.ReadAsStringAsync());
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
            var newFlight = new NewFlight
            {
                Departure = flight.Departure,
                FromId = flight.From.Id,
                ToId = flight.To.Id
            };
            
            var response = await Client.PostAsync(
                $"{_url}/flights", 
                new StringContent(JsonConvert.SerializeObject(newFlight))
            );
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateFlightAsync(Flight flight)
        {
            var newFlight = new NewFlight
            {
                Departure = flight.Departure,
                FromId = flight.From.Id,
                ToId = flight.To.Id
            };
            
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