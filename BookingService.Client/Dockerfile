FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build

WORKDIR /app

COPY BookingService.Client.csproj .
RUN dotnet restore BookingService.Client.csproj

COPY . .
RUN dotnet publish -c Release -o out