# =========================
#  STAGE 1 : Build (.NET 9)
# =========================
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# On copie le fichier csproj
COPY ["DeckBuilder.csproj", "./"]

# Restore des dépendances
RUN dotnet restore "DeckBuilder.csproj"

# On copie le reste du code source
COPY . .

# Publish en Release dans /app/publish
RUN dotnet publish "DeckBuilder.csproj" -c Release -o /app/publish /p:UseAppHost=false

# =========================
#  STAGE 2 : Runtime (.NET 9)
# =========================
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# User non-root (optionnel mais propre)
RUN useradd -m appuser
USER appuser

# On récupère les fichiers publiés depuis le stage build
COPY --from=build /app/publish .

# Exposer le port HTTP
EXPOSE 8080

# Lancer l'API
ENTRYPOINT ["dotnet", "DeckBuilder.dll"]
