FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["GestionCompetences.API/GestionCompetences.API.csproj", "GestionCompetences.API/"]
COPY ["GestionCompetences.Application/GestionCompetences.Application.csproj", "GestionCompetences.Application/"]
COPY ["GestionCompetences.Domain/GestionCompetences.Domain.csproj", "GestionCompetences.Domain/"]
COPY ["GestionCompetences.Infrastructure/GestionCompetences.Infrastructure.csproj", "GestionCompetences.Infrastructure/"]
RUN dotnet restore "GestionCompetences.API/GestionCompetences.API.csproj"

COPY . .
RUN dotnet publish "GestionCompetences.API/GestionCompetences.API.csproj" -c Release -o /app/publish 

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

COPY --from=build /app/publish .
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "GestionCompetences.API.dll"]