using System;

namespace BookingService.Client.Models
{
    public class Flight
    {
        public int      Id        { get; set; }
        public DateTime Departure { get; set; }
        public Country  From      { get; set; }
        public Country  To        { get; set; }
    }
}