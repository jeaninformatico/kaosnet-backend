# Imagen base ligera con .NET 8 sobre Alpine
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
WORKDIR /app
EXPOSE 5030

# Etapa de construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /src

LABEL maintainer="kaosnet@tuempresa.com" \
      description="KaosNet API - .NET 8 (Imagen ligera Alpine)"

# Copiar todo el proyecto desde el inicio
COPY . .

# Restaurar y publicar
RUN dotnet restore "KaosNetApi.csproj"
RUN dotnet publish "KaosNetApi.csproj" -c Release -o /app/publish

# Imagen final
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

# ENTRYPOINT para iniciar la app
ENTRYPOINT ["dotnet", "KaosNetApi.dll"]
