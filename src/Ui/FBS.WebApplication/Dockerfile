#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Install NODE for Angular Server
RUN curl -sL https://deb.nodesource.com/setup_10.x | bash -
RUN apt-get install --assume-yes nodejs


FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Ui/FBS.WebApplication/FBS.WebApplication.csproj", "src/Ui/FBS.WebApplication/"]
RUN dotnet restore "src/Ui/FBS.WebApplication/FBS.WebApplication.csproj"
COPY . .
WORKDIR "/src/src/Ui/FBS.WebApplication"
RUN dotnet build "FBS.WebApplication.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FBS.WebApplication.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FBS.WebApplication.dll"]