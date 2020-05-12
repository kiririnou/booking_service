namespace BookingService.WebApi.Contracts.V1
{
    // TODO: check maybe something needs to be deleted
    // TODO: add authorization for Countries, Flights, Users
    // TODO: maybe move previous TODO to another place? 
    public static class ApiRoutes
    {
        public const string Root        = "api";
        public const string Version     = "v1";
        public const string Base        = Root + "/" + Version;

        public static class Country
        {
            public const string GetAll  = Base + "/countries";
            public const string Create  = Base + "/countries";
            public const string Get     = Base + "/countries/{id}";
            public const string Update  = Base + "/countries/{id}";
            public const string Delete  = Base + "/countries/{id}";
        }

        public static class Flight
        {
            public const string GetAll  = Base + "/flights";
            public const string Create  = Base + "/flights";
            public const string Get     = Base + "/flights/{id}";
            public const string Update  = Base + "/flights/{id}";
            public const string Delete  = Base + "/flights/{id}";
        }

        public static class Reservation
        {
            public const string GetAll  = Base + "/reservations";
            public const string Create  = Base + "/reservations";
            public const string Get     = Base + "/reservations/{id}";
            public const string Update  = Base + "/reservations/{id}";
            public const string Delete  = Base + "/reservations/{id}";
        }

        public static class User
        {
            public const string GetAll  = Base + "/users";
            public const string Create  = Base + "/users";
            public const string Get     = Base + "/users/{id}";
            public const string Update  = Base + "/users/{id}";
            public const string Delete  = Base + "/users/{id}";
        }
    }
}