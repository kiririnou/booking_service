using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using BookingService.Client.Models;
using Newtonsoft.Json;

namespace BookingService.Client
{
    public partial class ApiClientWrapper : IUser, ICountry, IFlight, IReservation
    {
        private static readonly HttpClient Client = new HttpClient();
        private readonly string URL;

        private List<Reservation>         _reservations = new List<Reservation>();
        private List<Flight>              _flights      = new List<Flight>();
        private List<Country>             _countries    = new List<Country>();
        private List<User>                _users        = new List<User>();
        
        public IReadOnlyList<Reservation> Reservations { get => _reservations.AsReadOnly(); }
        public IReadOnlyList<Flight>      Flights      { get => _flights.AsReadOnly(); }
        public IReadOnlyList<Country>     Countries    { get => _countries.AsReadOnly(); }
        public IReadOnlyList<User>        Users        { get => _users.AsReadOnly(); }
        
        public ApiClientWrapper(string url)
        {
            URL = url;
        }
    }
}
