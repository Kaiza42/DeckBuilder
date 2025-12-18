FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["DeckBuilder.Api.csproj", "./"]

RUN dotnet restore "DeckBuilder.Api.csproj"

COPY . .

RUN dotnet publish "DeckBuilder.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

RUN useradd -m appuser
USER appuser

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "DeckBuilder.Api.dll"]
