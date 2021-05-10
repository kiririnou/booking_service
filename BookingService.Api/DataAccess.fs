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

let getAllEntitiesFromTable 
                (table : string) 
                (func  : RowReader -> 'T) 
                : Task<'T list> =
    connectionString
    |> Sql.connect
    |> Sql.query $"SELECT * FROM {table}"
    |> Sql.executeAsync func

let getEntityById 
                (id     : int)
                (table  : string)
                (func   : RowReader -> 'T)
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
                (func    : RowReader -> 'T)
                : Task<'T> =
    let _columns = columns |> String.concat ", "
    let _parameterNames = parameterNames |> String.concat ", "

    connectionString
    |> Sql.connect
    |> Sql.query $"INSERT INTO {table} ({_columns}) VALUES ({_parameterNames}) RETURNING *" 
    |> Sql.parameters parameters
    |> Sql.executeRowAsync func

// ============== User ==============
let getAllUsers () =
    getAllEntitiesFromTable 
        "users" 
        (fun read ->
            {
                Id = read.int "id"
                Name = read.text "name"
                TgUid = read.text "telegramid"
            })

let getUserById (id : int) =
    getEntityById 
        id 
        "users" 
        (fun read -> 
            {
                Id = read.int "id"
                Name = read.text "name"
                TgUid = read.text "telegramid"
            })

let insertUser (user : CreateOrUpdateUserRequest) = 
    insertEntityToTable
        "users"
        [ "Name";  "TelegramId"  ]
        [ "@name"; "@telegramId" ]
        [ "@name",       Sql.text user.Name; 
          "@telegramId", Sql.text user.TgUid ]
        (fun read ->
            {
                Id = read.int "id"
                Name = read.text "name"
                TgUid = read.text "telegramid"
            })

// ============== Country ==============

// ============== Flight ===============

// ============ Reservation ============
