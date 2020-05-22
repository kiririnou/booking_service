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
        private readonly string _url;

        public ApiClientWrapper(string url)
        {
            _url = url + "/api/v1";
        }
    }
}
