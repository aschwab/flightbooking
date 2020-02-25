#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Application/FBS.Authorization.API/FBS.Authorization.API.csproj", "src/Application/FBS.Authorization.API/"]
RUN dotnet restore "src/Application/FBS.Authorization.API/FBS.Authorization.API.csproj"
COPY . .
WORKDIR "/src/src/Application/FBS.Authorization.API"
RUN dotnet build "FBS.Authorization.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FBS.Authorization.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FBS.Authorization.API.dll"]