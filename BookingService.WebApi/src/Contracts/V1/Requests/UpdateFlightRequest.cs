namespace BookingService.WebApi.Contracts.V1.Requests
{
    public class UpdateFlightRequest
    {
        public string Departure { get; set; }

        public int FromId                { get; set; }

        public int ToId                  { get; set; }
    }
}