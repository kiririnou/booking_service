module BookingService.Contracts

open System

// Requests
type CreateOrUpdateUserRequest = {
    Name          : string
    TelegramId    : string
}

type CreateOrUpdateCountryRequest = {
    Name          : string
}

type CreateOrUpdateFlightRequest = {
    DepartureTime : DateTime
    FromCountry   : string
    ToCountry     : string
}

type CreateOrUpdateReservationRequest = {
    Flight        : string
    User          : string
}

// Responses
type UserResponse = {
    Id            : int
    Name          : string
    TelegramId    : string
}

type CountryResponse = {
    Id            : int
    Name          : string
}

type FlightResponse = {
    Id            : int
    DepartureTime : DateTime
    FromCountry   : CountryResponse
    ToCountry     : CountryResponse
}

type ReservationResponse = {
    Id            : int
    Flight        : FlightResponse
    User          : UserResponse
}
