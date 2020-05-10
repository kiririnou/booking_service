using System.ComponentModel.DataAnnotations;

namespace BookingService.WebApi.Models
{
    public class Reservation
    {
        [Key]
        public int Id               { get; set; }

        public int? FlightId        { get; set; }
        public Flight Flight        { get; set; }

        public int? UserId          { get; set; }
        public User User            { get; set; }
    }
}