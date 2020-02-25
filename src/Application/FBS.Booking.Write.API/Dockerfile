FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY src/Application/FBS.Booking.Write.API/FBS.Booking.Write.API.csproj src/Application/FBS.Booking.Write.API/
RUN dotnet restore "src/Application/FBS.Booking.Write.API/FBS.Booking.Write.API.csproj"
COPY . .
WORKDIR "/src/src/Application/FBS.Booking.Write.API"
RUN dotnet build "FBS.Booking.Write.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FBS.Booking.Write.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FBS.Booking.Write.API.dll"]
