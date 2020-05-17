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
        public async Task<List<Reservation>> GetReservationsAsync()
        {
            var response = await Client.GetStringAsync($"{_url}/reservations");
            var reservations = JsonConvert.DeserializeObject<List<Reservation>>(response);
            return reservations;
        }

        public async Task<Reservation> GetReservationByIdAsync(int id)
        {
            var response = await Client.GetAsync($"{_url}/reservations/{id.ToString()}");
            if (!response.IsSuccessStatusCode)
                return null;
            var reservation = JsonConvert.DeserializeObject<Reservation>(await response.Content.ReadAsStringAsync());
            return reservation;
        }
        
        public async Task<bool> CreateReservationAsync(Reservation reservation)
        {
            var response = await Client.PostAsync(
                $"{_url}/reservations", 
                new StringContent(JsonConvert.SerializeObject(reservation))
            );
            return response.IsSuccessStatusCode;
        }
        
        public async Task<bool> UpdateReservationAsync(Reservation newReservation)
        {
            var response = await Client.PutAsync(
                $"{_url}/reservations",
                new StringContent(JsonConvert.SerializeObject(newReservation))
            );
            return response.IsSuccessStatusCode;
        }
        
        public async Task<bool> DeleteReservationAsync(int id)
        {
            var response = await Client.DeleteAsync($"{_url}/reservations/{id.ToString()}");
            return response.IsSuccessStatusCode;
        }
    }
}