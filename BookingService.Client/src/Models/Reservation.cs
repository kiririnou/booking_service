namespace BookingService.Client.Models
{
    public class Reservation
    {
        public int    Id     { get; set; }
        public Flight Flight { get; set; }
        public User   User   { get; set; }
    }
}