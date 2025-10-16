# Imagen base ligera con .NET 8 sobre Alpine
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Etapa de construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /src

LABEL maintainer="kaosnet@tuempresa.com" \
      description="KaosNet API - .NET 8 (Imagen ligera Alpine)"

COPY ["KaosNetApi.csproj", "./"]
RUN dotnet restore "KaosNetApi.csproj"

COPY . .
RUN dotnet publish "KaosNetApi.csproj" -c Release -o /app/publish

# Imagen final
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "KaosNetApi.dll"]
