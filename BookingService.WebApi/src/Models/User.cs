using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookingService.WebApi.Models
{
    public class User
    {
        [Key]
        public int Id               { get; set; }
        public string Name          { get; set; }
        public string TgUid        { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}