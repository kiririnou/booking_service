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
        public async Task<List<Country>> GetCountriesAsync()
        {
            var response = await Client.GetAsync($"{_url}/countries");
            if (!response.IsSuccessStatusCode)
                return null;
            var countries = JsonConvert.DeserializeObject<List<Country>>(await response.Content.ReadAsStringAsync());
            return countries;
        }

        public async Task<Country> GetCountryByIdAsync(int id)
        {
            var response = await Client.GetAsync($"{_url}/countries/{id.ToString()}");
            if (!response.IsSuccessStatusCode)
                return null;
            var country = JsonConvert.DeserializeObject<Country>(await response.Content.ReadAsStringAsync());
            return country;
        }

        public async Task<bool> CreateCountryAsync(Country country)
        {
            var newCountry = new NewCountry
            {
                Name = country.Name
            };
            
            var response = await Client.PostAsync(
                $"{_url}/countries", 
                new StringContent(JsonConvert.SerializeObject(newCountry))
            );
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateCountryAsync(Country country)
        {
            var newCountry = new NewCountry
            {
                Name = country.Name
            }; 
            
            var response = await Client.PutAsync(
                $"{_url}/countries",
                new StringContent(JsonConvert.SerializeObject(newCountry))
            );
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCountryAsync(int id)
        {
            var response = await Client.DeleteAsync($"{_url}/countries/{id.ToString()}");
            return response.IsSuccessStatusCode;
        }
    }
}