namespace BookingService.WebApi.Contracts.V1.Requests
{
    public class CreateFlightRequest
    {
        public System.DateTime Depature { get; set; }

        public int FromId               { get; set; }

        public int ToId                 { get; set; }
    }
}