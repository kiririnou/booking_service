using System.Collections.Generic;
using BookingService.WebApi.Models;

namespace BookingService.WebApi.Contracts.V1.Responses
{
    public class CountryResponse
    {
        public int Id               { get; set; }
        public string Name          { get; set; }
    }
}