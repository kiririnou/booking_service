FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build
WORKDIR /app

COPY BookingService.TgBot.csproj .
RUN dotnet restore BookingService.TgBot.csproj

COPY . .
RUN dotnet publish -c Release -o out
COPY appsettings.json out/

FROM mcr.microsoft.com/dotnet/core/runtime:3.1 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

ENTRYPOINT ["dotnet", "BookingService.TgBot.dll"]