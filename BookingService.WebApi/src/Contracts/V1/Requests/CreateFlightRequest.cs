using System.ComponentModel.DataAnnotations;

namespace BookingService.WebApi.Contracts.V1.Requests
{
    public class CreateFlightRequest
    {
        [DataType(DataType.DateTime)]
        public string Departure         { get; set; }

        public int FromId               { get; set; }

        public int ToId                 { get; set; }
    }
}