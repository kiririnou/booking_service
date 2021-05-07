module BookingService.Handler

open Microsoft.AspNetCore.Http
open FSharp.Control.Tasks
open Giraffe
open BookingService.Models
open BookingService.Contracts
open BookingService.DataAccess

let notFound = {| Error = "Not Found" |}

// ============== Test ==============
let handleGetUsersTest = 
    fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
            let users = 
                getAllUsersTest
                |> Async.AwaitTask
                |> Async.RunSynchronously
                |> List.map (
                    fun (u : Models.User) -> { 
                        Id = u.Id; 
                        Name = u.Name; 
                        TelegramId = u.TelegramId 
                    })
            return! json users next ctx
        }

let handleGetUserByIdTest (id : int) = 
    fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
            let user = 
                getUserByIdTest id
                |> Async.AwaitTask
                |> Async.RunSynchronously
                |> List.tryHead
                |> fun head -> 
                    match head with
                    | Some u -> { Id = u.Id; Name = u.Name; TelegramId = u.TelegramId }
                    | None   -> { Id = -1; Name = ""; TelegramId = "" }

            return! if user.Id = -1 then json notFound next ctx
                    else json user next ctx
        }

let handleCreateUserTest =
    fun (next : HttpFunc) (ctx : HttpContext) -> 
        task {
            let! user = ctx.BindJsonAsync<CreateOrUpdateUserRequest>()
            let! _ = insertUserTest user
            return! json user next ctx
        }
// ============== User ==============

let handleGetUsers = 
    fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
            let users = 
                getAllUsers 
                |> Async.AwaitTask
                |> Async.RunSynchronously
                |> List.map (
                    fun (u : Models.User) -> { 
                        Id = u.Id; 
                        Name = u.Name; 
                        TelegramId = u.TelegramId 
                    })
            return! json users next ctx
        }

let handleGetUserById (id : int) : HttpHandler =
    fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
            //let! users = (getUserById id)
            let user = 
                getUserById id
                |> Async.AwaitTask
                |> Async.RunSynchronously
                |> List.tryHead
                |> fun head -> 
                    match head with
                    | Some u -> { Id = u.Id; Name = u.Name; TelegramId = u.TelegramId }
                    | None   -> { Id = -1; Name = ""; TelegramId = "" }

            return! if user.Id = -1 then json notFound next ctx
                    else json user next ctx
        }

let handleCreateUser =
    fun (next : HttpFunc) (ctx : HttpContext) -> 
        task {
            let! user = ctx.BindJsonAsync<CreateOrUpdateUserRequest>()
            let! _ = createUser user
            return! json user next ctx
        }

// ============== Country ==============

// ============== Flight ===============

// ============ Reservation ============
