namespace BookingService.WebApi.Contracts.V1.Responses
{
    public class ReservationResponse
    {
        public int Id                    { get; set; }
        
        public FlightResponse Flight     { get; set; }
        
        public UserResponse User         { get; set; }
    }
}