# Establecer la imagen base
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Establecer la imagen de compilación
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BlazorAppOrquesta.csproj", ""]
RUN dotnet restore "./BlazorAppOrquesta.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "BlazorAppOrquesta.csproj" -c Release -o /app/build

# Establecer la imagen de publicación
FROM build AS publish
RUN dotnet publish "BlazorAppOrquesta.csproj" -c Release -o /app/publish

# Configurar la imagen de ejecución
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlazorAppOrquesta.dll"]
