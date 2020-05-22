using System;

namespace BookingService.Client.Models
{
    public class NewFlight
    {
        public DateTime Departure { get; set; }
        public int      FromId    { get; set; }
        public int      ToId      { get; set; }
    }
}