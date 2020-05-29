using System;
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
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly IFlightService _flightService;
        private readonly ICountryService _countryService;
        private readonly IUserService _userService;

        public ReservationController(IReservationService reservationService, ICountryService countryService, IUserService userService, IFlightService flightService)
        {
            _reservationService = reservationService;
            _countryService = countryService;
            _userService = userService;
            _flightService = flightService;
        }

        [HttpGet(ApiRoutes.Reservation.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var reservations = await _reservationService.GetReservationsAsync();
            var reservationResponse = reservations.Select(reservation => new ReservationResponse
            {
                Id = reservation.Id,
                Flight = new FlightResponse
                {
                    Id = reservation.Flight.Id,
                    Departure = reservation.Flight.Departure,
                    From = new CountryResponse
                    {
                        Id = reservation.Flight.From.Id,
                        Name = reservation.Flight.From.Name
                    },
                    To = new CountryResponse
                    {
                        Id = reservation.Flight.To.Id,
                        Name = reservation.Flight.To.Name
                    }
                },
                User = new UserResponse
                {
                    Id = reservation.User.Id,
                    Name = reservation.User.Name,
                    TgUid = reservation.User.TgUid
                }
            }).ToList();
            return Ok(reservationResponse);
        }

        [HttpGet(ApiRoutes.Reservation.Get)]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var reservation = await _reservationService.GetReservationByIdAsync(id);
            if (reservation == null)
                return NotFound();
            return Ok(new ReservationResponse
            {
                Id = reservation.Id,
                Flight = new FlightResponse
                {
                    Id = reservation.Flight.Id,
                    Departure = reservation.Flight.Departure,
                    From = new CountryResponse
                    {
                        Id = reservation.Flight.From.Id,
                        Name = reservation.Flight.From.Name
                    },
                    To = new CountryResponse
                    {
                        Id = reservation.Flight.To.Id,
                        Name = reservation.Flight.To.Name
                    }
                },
                User = new UserResponse
                {
                    Id = reservation.User.Id,
                    Name = reservation.User.Name,
                    TgUid = reservation.User.TgUid
                }
            });
        }

        [HttpPost(ApiRoutes.Reservation.Create)]
        public async Task<IActionResult> Post([FromBody] CreateReservationRequest request)
        {
            var reservation = new Reservation
            {
                FlightId = request.FlightId,
                Flight = await _flightService.GetFlightByIdAsync(request.FlightId),
                UserId =  request.UserId,
                User = await _userService.GetUserByIdAsync(request.UserId)
            };

            await _reservationService.CreateReservationAsync(reservation);

            var url = String.Format(
                "{0}://{1}{2}",
                HttpContext.Request.Scheme,
                HttpContext.Request.Host.ToUriComponent(),
                ApiRoutes.Reservation.Get.Replace("{id}", reservation.Id.ToString())
            );

            var response = new ReservationResponse
            {
                Id = reservation.Id,
                Flight = new FlightResponse
                {
                    Id = reservation.Flight.Id,
                    Departure = reservation.Flight.Departure,
                    From = new CountryResponse
                    {
                        Id = reservation.Flight.From.Id,
                        Name = reservation.Flight.From.Name
                    },
                    To = new CountryResponse
                    {
                        Id = reservation.Flight.To.Id,
                        Name = reservation.Flight.To.Name
                    }
                },
                User = new UserResponse
                {
                    Id = reservation.User.Id,
                    Name = reservation.User.Name,
                    TgUid = reservation.User.TgUid
                }
            };

            return Created(url, response);
        }

        [HttpPut(ApiRoutes.Reservation.Update)]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] UpdateReservationRequest request)
        {
            var reservation = new Reservation
            {
                Id = id,
                FlightId = request.FlightId,
                UserId = request.UserId
            };

            if (await _reservationService.UpdateReservationAsync(reservation))
                return Ok(new ReservationResponse
            {
                Id = reservation.Id,
                Flight = new FlightResponse
                {
                    Id = reservation.Flight.Id,
                    Departure = reservation.Flight.Departure,
                    From = new CountryResponse
                    {
                        Id = reservation.Flight.From.Id,
                        Name = reservation.Flight.From.Name
                    },
                    To = new CountryResponse
                    {
                        Id = reservation.Flight.To.Id,
                        Name = reservation.Flight.To.Name
                    }
                },
                User = new UserResponse
                {
                    Id = reservation.User.Id,
                    Name = reservation.User.Name,
                    TgUid = reservation.User.TgUid
                }
            });
            return NotFound();
        }

        [HttpDelete(ApiRoutes.Reservation.Delete)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (await _reservationService.DeleteReservationAsync(id))
                return NoContent();
            return NotFound();
        }
    }
}