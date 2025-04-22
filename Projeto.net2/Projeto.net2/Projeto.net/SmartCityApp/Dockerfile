# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copiar tudo da pasta atual
COPY . .

# Restaurar, compilar e publicar
RUN dotnet restore SmartCityApp.csproj
RUN dotnet publish SmartCityApp.csproj -c Release -o /app/publish

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "SmartCityApp.dll"]
