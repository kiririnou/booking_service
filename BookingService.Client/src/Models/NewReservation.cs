using Newtonsoft.Json;

namespace BookingService.Client.Models
{
    public class NewReservation
    {
        [JsonProperty(PropertyName = "flightid")]
        public int FlightId { get; set; }
        [JsonProperty(PropertyName = "userid")]
        public int UserId   { get; set; }
    }
}
