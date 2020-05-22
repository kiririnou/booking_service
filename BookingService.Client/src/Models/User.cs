using System;
using System.Net.Http;
using System.Runtime.CompilerServices;

namespace BookingService.Client.Models
{
    public class User
    {
        public int    Id    { get; set; }
        public string Name  { get; set; }
        public string TgUid { get; set; }
    }
}