module BookingService.App

open System
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.Logging
open Microsoft.Extensions.DependencyInjection
open Giraffe
open BookingService.Handler

let webApp = 
    choose [
        subRouteCi "/api"
            (choose [
                subRouteCi "/users" 
                    (choose [
                        GET >=> choose[
                            routex "(/?)" >=> handleGetUsers
                            routef "/%i" handleGetUserById
                        ]
                        POST >=> choose [
                            routex "(/?)" >=> handleCreateUser
                        ]
                    ])
            ])
        RequestErrors.notFound (json notFound)
    ]

let errorHandler (ex : Exception) (logger : ILogger) =
    logger.LogError(ex, "An unhandled exception has occurred while executing the request.")
    clearResponse >=> setStatusCode 500 >=> text ex.Message

let configureApp (app : IApplicationBuilder) =
    app.UseGiraffeErrorHandler(errorHandler)
       .UseGiraffe webApp

let configureServices (services : IServiceCollection) =
    services.AddGiraffe() |> ignore

let configureLogging (logBuilder : ILoggingBuilder) = 
    logBuilder.AddConsole()
              .AddDebug() |> ignore

[<EntryPoint>]
let main _ =
    Host.CreateDefaultBuilder()
        .ConfigureWebHostDefaults(
            fun webHostBuilder ->
                webHostBuilder
                    .UseKestrel()
                    .Configure(configureApp)
                    .ConfigureServices(configureServices)
                    .ConfigureLogging(configureLogging)
                    |> ignore
            )
        .Build()
        .Run()
    0
