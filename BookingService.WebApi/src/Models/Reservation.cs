using System.ComponentModel.DataAnnotations;

namespace BookingService.WebApi.Models
{
    public class Reservation
    {
        [Key]
        public int Id               { get; set; }

        public int? FlightId        { get; set; }
        public virtual Flight Flight        { get; set; }

        public int? UserId          { get; set; }
        public virtual User User            { get; set; }
    }
}