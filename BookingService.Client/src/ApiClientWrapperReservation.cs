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
            var response = await Client.GetAsync($"{_url}/reservations");
            if (!response.IsSuccessStatusCode)
                return null;
            var reservations = JsonConvert.DeserializeObject<List<Reservation>>(await response.Content.ReadAsStringAsync());
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
            var newReservation = new NewReservation
            {
                FlightId = reservation.Flight.Id,
                UserId = reservation.User.Id
            };
            
            var response = await Client.PostAsync(
                $"{_url}/reservations", 
                new StringContent(JsonConvert.SerializeObject(newReservation))
            );
            return response.IsSuccessStatusCode;
        }
        
        public async Task<bool> UpdateReservationAsync(Reservation reservation)
        {
            var newReservation = new NewReservation
            {
                FlightId = reservation.Flight.Id,
                UserId = reservation.User.Id
            };
            
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