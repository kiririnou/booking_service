namespace BookingService.WebApi.Contracts.V1.Requests
{
    public class UpdateFlightRequest
    {
        public System.DateTime Departure { get; set; }

        public int FromId                { get; set; }

        public int ToId                  { get; set; }
    }
}