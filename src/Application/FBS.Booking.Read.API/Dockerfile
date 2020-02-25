FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY src/Application/FBS.Booking.Read.API/FBS.Booking.Read.API.csproj src/Application/FBS.Booking.Read.API/
RUN dotnet restore "src/Application/FBS.Booking.Read.API/FBS.Booking.Read.API.csproj"
COPY . .
WORKDIR "/src/src/Application/FBS.Booking.Read.API"
RUN dotnet build "FBS.Booking.Read.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FBS.Booking.Read.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FBS.Booking.Read.API.dll"]
