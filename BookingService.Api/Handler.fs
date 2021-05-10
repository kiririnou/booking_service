module BookingService.Handler

open Microsoft.AspNetCore.Http
open FSharp.Control.Tasks
open Giraffe
open BookingService.Contracts
open BookingService.DataAccess.Users
open BookingService.DataAccess.Countries
open BookingService.DataAccess.Flights
open BookingService.DataAccess.Reservations

let notFound = {| Error = "Not Found" |}

// ============== User ==============

let handleGetUsers = 
    fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
            let! users = getAllUsers ()

            return! json users next ctx
        }

let handleGetUserById (id : int) =
    fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
            let user = 
                getUserById id
                |> Async.AwaitTask
                |> Async.RunSynchronously
                |> List.tryHead

            return! if user.IsSome then json user.Value next ctx
                    else json notFound next ctx
        }

let handleCreateUser =
    fun (next : HttpFunc) (ctx : HttpContext) -> 
        task {
            let! user = ctx.BindJsonAsync<CreateOrUpdateUserRequest>()
            let! inserted = insertUser user

            return! json inserted next ctx
        }

// ============== Country ==============

let handleGetCountries =
    fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
            let! countries = getAllCountries ()

            return! json countries next ctx
        }

let handleGetCountryById (id : int) = 
    fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
            let country = 
                getCountryById id
                |> Async.AwaitTask
                |> Async.RunSynchronously
                |> List.tryHead

            return! if country.IsSome then json country.Value next ctx
                    else json notFound next ctx
        }

let handleCreateCountry =
    fun (next : HttpFunc) (ctx : HttpContext) -> 
        task {
            let! country = ctx.BindJsonAsync<CreateOrUpdateCountryRequest>()
            let! inserted = insertCountry country

            return! json inserted next ctx
        }

// ============== Flight ===============

let handleGetFlights =
    fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
            let! flights = getAllFlights ()
            //let flights = []


            return! json flights next ctx
        }

let handleGetFlightById (id : int) = 
    fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
            let flight = 
                getFlightById id
                |> Async.AwaitTask
                |> Async.RunSynchronously
                |> List.tryHead

            return! if flight.IsSome then json flight.Value next ctx
                    else json notFound next ctx
        }

let handleCreateFlight =
    fun (next : HttpFunc) (ctx : HttpContext) -> 
        task {
            let! flight = ctx.BindJsonAsync<CreateOrUpdateFlightRequest>()
            let! inserted = insertFlight flight

            return! json inserted next ctx
        }

// ============ Reservation ============

let handleGetReservations =
    fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
            let! reservation = getAllReservations ()

            return! json reservation next ctx
        }

let handleGetReservationById (id : int) = 
    fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
            let reservation = 
                getReservationById id
                |> Async.AwaitTask
                |> Async.RunSynchronously
                |> List.tryHead

            return! if reservation.IsSome then json reservation.Value next ctx
                    else json notFound next ctx
        }

let handleCreateReservation =
    fun (next : HttpFunc) (ctx : HttpContext) -> 
        task {
            let! reservation = ctx.BindJsonAsync<CreateOrUpdateReservationRequest>()
            let! inserted = insertReservation reservation

            return! json inserted next ctx
        }
