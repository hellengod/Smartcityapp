# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copiar tudo da pasta atual
COPY . .

# Entrar na pasta correta antes de restaurar
WORKDIR /app/Projeto.net2/Projeto.net2/Projeto.net/SmartCityApp
RUN dotnet restore SmartCityApp.csproj
RUN dotnet publish SmartCityApp.csproj -c Release -o /app/publish

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "SmartCityApp.dll"]