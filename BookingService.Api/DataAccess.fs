module BookingService.DataAccess

open Npgsql.FSharp
open BookingService.Models
open BookingService.Contracts
open System.Threading.Tasks

let connectionString = 
    Sql.host "192.168.1.122"
    |> Sql.database "FlightDB"
    |> Sql.username "postgres"
    |> Sql.password "postgres"
    |> Sql.port 5432
    |> Sql.formatConnectionString

let getAllEntitiesFromTable 
                    (table : string) 
                    (func  : RowReader -> 'T) 
                    : Task<'T list> =
    connectionString
    |> Sql.connect
    |> Sql.query $"SELECT * FROM {table}"
    |> Sql.executeAsync func

let getEntityById 
        (id : int)
        (table : string)
        (func  : RowReader -> 'T)
        : Task<'T list> =
    connectionString
    |> Sql.connect
    |> Sql.query $"SELECT * FROM {table} WHERE Id = @id"
    |> Sql.parameters [ "@id", Sql.int id ]
    |> Sql.executeAsync func

let insertEntityToTable
                    (table   : string)
                    (columns : string list)
                    (parameterNames : string list)
                    (parameters     : (string * SqlValue) list)
                    : Task<int> =
    connectionString
    |> Sql.connect
    |> Sql.query (sprintf "INSERT INTO %s (%s) VALUES (%s)" 
                           table 
                           (columns        |> String.concat ", ") 
                           (parameterNames |> String.concat ", "))
    |> Sql.parameters parameters
    |> Sql.executeNonQueryAsync

// ============== Test ==============
let getAllUsersTest =
    getAllEntitiesFromTable 
        "Users" 
        (fun read ->
            {
                Id = read.int "id"
                Name = read.text "name"
                TelegramId = read.text "telegramid"
            } : User)

let getUserByIdTest (id : int) =
    getEntityById 
        id 
        "Users" 
        (fun read -> 
            {
                Id = read.int "id"
                Name = read.text "name"
                TelegramId = read.text "telegramid"
            } : User)

let insertUserTest (user : CreateOrUpdateUserRequest) = 
    insertEntityToTable
        "Users"
        [ "Name";  "TelegramId"  ]
        [ "@name"; "@telegramId" ]
        [ "@name",       Sql.text user.Name; 
          "@telegramId", Sql.text user.TelegramId ]

// ============== User ==============

let getAllUsers : Task<User list> =
    connectionString
    |> Sql.connect
    |> Sql.query "SELECT * FROM Users"
    |> Sql.executeAsync (fun read ->
        {
            Id = read.int "id"
            Name = read.text "name"
            TelegramId = read.text "telegramid"
        })

let getUserById (id : int) : Task<User list> =
    connectionString
    |> Sql.connect
    |> Sql.query "SELECT * FROM Users WHERE Id = @id"
    |> Sql.parameters [ "@id", Sql.int id ]
    |> Sql.executeAsync (fun read -> 
        {
            Id = read.int "id"
            Name = read.text "name"
            TelegramId = read.text "telegramid"
        })

let createUser (user : CreateOrUpdateUserRequest) =
    connectionString
    |> Sql.connect
    |> Sql.query @"
        INSERT INTO Users (Name, TelegramId) VALUES (@name, @telegramId)"
    |> Sql.parameters [ "@name", Sql.text user.Name; "@telegramId", Sql.text user.TelegramId ]
    |> Sql.executeNonQueryAsync

// ============== Country ==============

// ============== Flight ===============

// ============ Reservation ============
