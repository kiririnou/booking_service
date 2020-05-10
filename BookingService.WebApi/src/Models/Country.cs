using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookingService.WebApi.Models
{
    public class Country
    {
        [Key]
        public int Id               { get; set; }
        public string Name          { get; set; }

        public virtual ICollection<Flight> FlightsFrom   { get; set; }
        public virtual ICollection<Flight> FlightsTo     { get; set; }
    }
}