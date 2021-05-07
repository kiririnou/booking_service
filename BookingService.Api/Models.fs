module BookingService.Models

open System

type User = {
    Id          : int
    Name        : string
    TelegramId  : string
}

type Country = {
    Id    : int
    Name  : int
}

type Flight = {
    Id              : int
    DepartureTime   : DateTime
    FromCountryId   : int
    ToCountryId     : int
}

type Reservation = {
    Id        : int
    FlightId  : int
    UserId    : int
}
