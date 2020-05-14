namespace BookingService.WebApi.Contracts.V1.Requests
{
    public class UpdateReservationRequest
    {
        public int FlightId   { get; set; }
        public int UserId     { get; set; }
    }
}