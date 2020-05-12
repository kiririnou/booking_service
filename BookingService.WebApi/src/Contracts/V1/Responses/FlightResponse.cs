namespace BookingService.WebApi.Contracts.V1.Responses
{
    public class FlightResponse
    {
        public int Id                       { get; set; }
        
        public System.DateTime Departure    { get; set; }

        public CountryResponse From         { get; set; }
        
        public CountryResponse To           { get; set; }
    }
}