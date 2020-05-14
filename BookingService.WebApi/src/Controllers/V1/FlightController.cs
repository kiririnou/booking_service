using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using BookingService.WebApi.Contracts.V1;
using BookingService.WebApi.Contracts.V1.Requests;
using BookingService.WebApi.Contracts.V1.Responses;
using BookingService.WebApi.Models;
using BookingService.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.WebApi.Controllers
{
    public class FlightController : ControllerBase
    {
        private readonly IFlightService _flightService;
        private readonly ICountryService _countryService;
        
        public FlightController(IFlightService flightService, ICountryService countryService)
        {
            _flightService = flightService;
            _countryService = countryService;
        }
        
        [HttpGet(ApiRoutes.Flight.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var flights = await _flightService.GetFlightsAsync();
            var flightResponse = flights.Select(flight => new FlightResponse
            {
                Id = flight.Id,
                Departure = flight.Departure,
                From = new CountryResponse
                {
                    Id = flight.From.Id,
                    Name = flight.From.Name
                },
                To = new CountryResponse
                {
                    Id = flight.To.Id,
                    Name = flight.To.Name
                }
            }).ToList();
            return Ok(flightResponse);
        }

        [HttpGet(ApiRoutes.Flight.Get)]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var flight = await _flightService.GetFlightByIdAsync(id);
            if (flight == null)
                return NotFound();
            return Ok(new FlightResponse
            {
                Id = flight.Id,
                Departure = flight.Departure,
                From = new CountryResponse
                {
                    Id = flight.From.Id,
                    Name = flight.From.Name
                },
                To = new CountryResponse
                {
                    Id = flight.To.Id,
                    Name = flight.To.Name
                }
            });
        }

        [HttpPost(ApiRoutes.Flight.Create)]
        public async Task<IActionResult> Post([FromBody] CreateFlightRequest request)
        {
            if (request == null)
                throw new Exception("Request if fucking null");
            
            var flight = new Flight
            {
                Departure = DateTime.ParseExact(request.Departure, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                FromId = request.FromId,
                From = await _countryService.GetCountryByIdAsync(request.FromId),
                ToId = request.ToId,
                To = await _countryService.GetCountryByIdAsync(request.ToId)
            };

            await _flightService.CreateFlightAsync(flight);

            var url = String.Format(
                "{0}://{1}{2}",
                HttpContext.Request.Scheme,
                HttpContext.Request.Host.ToUriComponent(),
                ApiRoutes.Flight.Get.Replace("{id}", flight.Id.ToString())
            );

            var response = new FlightResponse
            {
                Id = flight.Id,
                Departure = flight.Departure,
                From = new CountryResponse
                {
                    Id = flight.From.Id,
                    Name = flight.From.Name
                },
                To = new CountryResponse
                {
                    Id = flight.To.Id,
                    Name = flight.To.Name
                }
            };

            return Created(url, response);
        }

        [HttpPut(ApiRoutes.Flight.Update)]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] UpdateFlightRequest request)
        {
            var flight = new Flight
            {
                Id = id,
                Departure = DateTime.ParseExact(request.Departure, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                FromId = request.FromId,
                ToId = request.ToId
            };

            if (await _flightService.UpdateFlightAsync(flight))
                return Ok(new FlightResponse
                {
                    Id = flight.Id,
                    Departure = flight.Departure,
                    From = new CountryResponse
                    {
                        Id = flight.From.Id,
                        Name = flight.From.Name
                    },
                    To = new CountryResponse
                    {
                        Id = flight.To.Id,
                        Name = flight.To.Name
                    }
                });
            return NotFound();
        }

        [HttpDelete(ApiRoutes.Flight.Delete)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (await _flightService.DeleteFlightAsync(id))
                return NoContent();
            return NotFound();
        }
    }
}