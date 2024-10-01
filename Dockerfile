# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

COPY *.sln .
COPY MovieCraft.Server/*.csproj ./MovieCraft.Server/
COPY MovieCraft.Client/*.csproj ./MovieCraft.Client/
COPY MovieCraft.Application/*.csproj ./MovieCraft.Application/
COPY MovieCraft.Infrastructure/*.csproj ./MovieCraft.Infrastructure/
COPY MovieCraft.Domain/*.csproj ./MovieCraft.Domain/
COPY MovieCraft.Shared/*.csproj ./MovieCraft.Shared/

RUN dotnet restore

COPY . .

# Build the client app
WORKDIR /source/MovieCraft.Client
RUN dotnet publish -c Release -o /app/client_build

# Build the server app
WORKDIR /source/MovieCraft.Server
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Use runtime to run the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy server app
COPY --from=build /app/publish .
# Copy client app to wwwroot
COPY --from=build /app/client_build/wwwroot ./wwwroot

# Configure HTTPS and SSL certificate
ENV ASPNETCORE_URLS="http://+:80;https://+:443"
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/https/localhost.pfx
ENV ASPNETCORE_Kestrel__Certificates__Default__Password=${SSL_CERT_PASSWORD}

# Expose HTTP and HTTPS ports
EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "MovieCraft.Server.dll"]
