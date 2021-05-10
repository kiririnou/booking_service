module BookingService.Handler

open Microsoft.AspNetCore.Http
open FSharp.Control.Tasks
open Giraffe
open BookingService.Contracts
open BookingService.DataAccess

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

// ============== Flight ===============

// ============ Reservation ============
