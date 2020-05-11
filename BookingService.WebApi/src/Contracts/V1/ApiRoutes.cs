namespace BookingService.WebApi.Contracts.V1
{
    // TODO: check maybe something needs to be deleted
    // TODO: add authorization Countries, Flights, Users
    // TODO: maybe move previous TODO to another place? 
    public static class ApiRoutes
    {
        public const string Root        = "api";
        public const string Version     = "v1";
        public const string Base        = Root + "/" + Version;

        public static class Countries
        {
            public const string GetAll  = Base + "/countries";
            public const string Create  = Base + "/countries";
            public const string Get     = Base + "/countries/{id}";
            public const string Update  = Base + "/countries/{id}";
            public const string Delete  = Base + "/countries/{id}";
        }

        public static class Flights
        {
            public const string GetAll  = Base + "/countries";
            public const string Create  = Base + "/countries";
            public const string Get     = Base + "/countries/{id}";
            public const string Update  = Base + "/countries/{id}";
            public const string Delete  = Base + "/countries/{id}";
        }

        public static class Reservations
        {
            public const string GetAll  = Base + "/countries";
            public const string Create  = Base + "/countries";
            public const string Get     = Base + "/countries/{id}";
            public const string Update  = Base + "/countries/{id}";
            public const string Delete  = Base + "/countries/{id}";
        }

        public static class Users
        {
            public const string GetAll  = Base + "/countries";
            public const string Create  = Base + "/countries";
            public const string Get     = Base + "/countries/{id}";
            public const string Update  = Base + "/countries/{id}";
            public const string Delete  = Base + "/countries/{id}";
        }
    }
}