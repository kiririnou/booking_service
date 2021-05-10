module BookingService.DataAccess

open Npgsql.FSharp
open BookingService.Contracts
open System.Threading.Tasks

let connectionString = 
    Sql.host "192.168.1.122"
    |> Sql.database "FlightDB"
    |> Sql.username "postgres"
    |> Sql.password "postgres"
    |> Sql.port 5432
    |> Sql.formatConnectionString

let getAllEntities
                (query : string)
                (read  : RowReader -> 'T) 
                : Task<'T list> =
    connectionString
    |> Sql.connect
    |> Sql.query query
    |> Sql.executeAsync read

let getEntityById 
                (id     : int)
                (table  : string)
                (read   : RowReader -> 'T)
                : Task<'T list> =
    connectionString
    |> Sql.connect
    |> Sql.query $"SELECT * FROM {table} WHERE Id = @id"
    |> Sql.parameters [ "@id", Sql.int id ]
    |> Sql.executeAsync read

let insertEntityToTable
                (table      : string)
                (columns    : string list)
                (paramNames : string list)
                (parameters : (string * SqlValue) list)
                (read       : RowReader -> 'T)
                : Task<'T> =
    let _columns = columns |> String.concat ", "
    let _parameterNames = paramNames |> String.concat ", "

    connectionString
    |> Sql.connect
    |> Sql.query $"INSERT INTO {table} ({_columns}) VALUES ({_parameterNames}) RETURNING *" 
    |> Sql.parameters parameters
    |> Sql.executeRowAsync read

let deleteEntityById
                (query      : string)
                (parameters : (string * SqlValue) list)
                : Task<int> =
    connectionString
    |> Sql.connect
    |> Sql.query query
    |> Sql.parameters parameters
    |> Sql.executeNonQueryAsync


// ============== User ==============
module Users = 
    
    let userReader (read : RowReader) =
        {
            Id = read.int "id"
            Name = read.text "name"
            TgUid = read.text "telegramid"
        }

    let getAllUsers () =
        getAllEntities $"SELECT * FROM users" userReader
    
    let getUserById (id : int) =
        getEntityById id "users" userReader
    
    let insertUser (user : CreateOrUpdateUserRequest) = 
        insertEntityToTable
            "users"
            [ "Name";  "TelegramId"  ]
            [ "@name"; "@telegramId" ]
            [ "@name",       Sql.text user.Name; 
              "@telegramId", Sql.text user.TgUid ]
            userReader
    
// ============== Country ==============
module Countries = 

    let countryReader (read : RowReader) = 
        {
            Id = read.int "id"
            Name = read.text "name"
        }

    let getAllCountries () =
        getAllEntities $"SELECT * FROM countries" countryReader

    let getCountryById (id : int) =
        getEntityById id "countries" countryReader

    let insertCountry (country : CreateOrUpdateCountryRequest) =
        insertEntityToTable
            "countries"
            [ "Name" ]
            [ "@name" ]
            [ "@name", Sql.text country.Name ]
            countryReader

// ============== Flight ===============
module Flights =
    let flightReader (read : RowReader) =
        {
            Id = read.int "id"
            Departure = read.dateTime "departuretime"
            From = read.int "fromcountryid" 
                |> Countries.getCountryById
                |> Async.AwaitTask
                |> Async.RunSynchronously
                |> List.head
            To = read.int "tocountryid" 
                |> Countries.getCountryById
                |> Async.AwaitTask
                |> Async.RunSynchronously
                |> List.head
        }

    let getAllFlights () =
        getAllEntities $"SELECT * FROM flights" flightReader

    let getFlightById (id : int) =
        getEntityById id "flights" flightReader

    let insertFlight (flight : CreateOrUpdateFlightRequest) =
        insertEntityToTable
            "flights"
            [ "DepartureTime"; "FromCountryId"; "ToCountryId" ]
            [ "@departuretime"; "@fromcountryid"; "@tocountryid" ]
            [ "@departuretime", Sql.timestamp (System.DateTime.Parse(flight.Departure));
              "@fromcountryid", Sql.int flight.FromId; 
              "@tocountryid",   Sql.int flight.ToId ]
            flightReader

// ============ Reservation ============
module Reservations = 
    let reservationReader (read : RowReader) =
        {
            Id = read.int "id"
            Flight = read.int "flightid"
                |> Flights.getFlightById
                |> Async.AwaitTask
                |> Async.RunSynchronously
                |> List.head
            User = read.int "userid"
                |> Users.getUserById
                |> Async.AwaitTask
                |> Async.RunSynchronously
                |> List.head
        }

    let getAllReservations () =
        getAllEntities $"SELECT * FROM reservations" reservationReader

    let getReservationById (id : int) =
        getEntityById id "reservations" reservationReader

    let insertReservation (reservation : CreateOrUpdateReservationRequest) =
        insertEntityToTable
            "reservations"
            [ "FlightId" ; "UserId"  ]
            [ "@flightid"; "@userid" ]
            [ "@flightid" , Sql.int reservation.FlightId; 
              "@userid"   , Sql.int reservation.UserId ]
            reservationReader