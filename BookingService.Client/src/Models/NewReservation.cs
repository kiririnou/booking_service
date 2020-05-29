using Newtonsoft.Json;

namespace BookingService.Client.Models
{
    public class NewReservation
    {
        [JsonProperty(PropertyName = "flightId")]
        public int FlightId { get; set; }
        [JsonProperty(PropertyName = "userId")]
        public int UserId   { get; set; }
    }
}
