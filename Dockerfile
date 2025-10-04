# Etapa de construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia todo el contenido del proyecto
COPY . .

# Restaura dependencias y publica el proyecto
RUN dotnet restore "Api-Kaos-Net/Api-Kaos-Net.csproj"
RUN dotnet publish "Api-Kaos-Net/Api-Kaos-Net.csproj" -c Release -o /app/publish

# Etapa de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Punto de entrada
ENTRYPOINT ["dotnet", "Api-Kaos-Net.dll"]
