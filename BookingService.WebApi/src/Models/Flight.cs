using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookingService.WebApi.Models
{
    public class Flight
    {
        [Key]
        public int Id               { get; set; }
        public DateTime Departure   { get; set; }

        public int? FromId          { get; set; }
        public Country From         { get; set; }

        public int? ToId            { get; set; }
        public Country To           { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
    }
}