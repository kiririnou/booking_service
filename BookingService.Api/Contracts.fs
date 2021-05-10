module BookingService.Contracts

open System

// Requests
type CreateOrUpdateUserRequest = {
    Name        : string
    TgUid       : string
}

type CreateOrUpdateCountryRequest = {
    Name        : string
}

type CreateOrUpdateFlightRequest = {
    //Departure       : DateTime
    Departure   : string
    FromId      : int
    ToId        : int
}

type CreateOrUpdateReservationRequest = {
    FlightId    : int
    UserId      : int
}

// Responses
type UserResponse = {
    Id          : int
    Name        : string
    TgUid       : string
}

type CountryResponse = {
    Id          : int
    Name        : string
}

type FlightResponse = {
    Id          : int
    Departure   : DateTime
    From        : CountryResponse
    To          : CountryResponse
}

type ReservationResponse = {
    Id          : int
    Flight      : FlightResponse
    User        : UserResponse
}
