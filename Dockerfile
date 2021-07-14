FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["AltaMiraDatabase.API/AltaMiraDatabase.API.csproj", "."]
COPY ["AltaMiraDatabase.Business/AltaMiraDatabase.Business.csproj", "."]
COPY ["AltaMiraDatabase.DataAccess/AltaMiraDatabase.DataAccess.csproj", "."]
COPY ["AltaMiraDatabase.Entities/AltaMiraDatabase.Entities.csproj", "."]
RUN dotnet restore "AltaMiraDatabase.API.csproj"
COPY . .
WORKDIR "/src/AltaMiraDatabase.API"
RUN dotnet build "AltaMiraDatabase.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AltaMiraDatabase.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AltaMiraDatabase.API.dll"]