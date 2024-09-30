# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

COPY *.sln .
COPY MovieCraft.Server/*.csproj ./MovieCraft.Server/
COPY MovieCraft.Application/*.csproj ./MovieCraft.Application/
COPY MovieCraft.Infrastructure/*.csproj ./MovieCraft.Infrastructure/
COPY MovieCraft.Domain/*.csproj ./MovieCraft.Domain/
COPY MovieCraft.Shared/*.csproj ./MovieCraft.Shared/

RUN dotnet restore

COPY . .
WORKDIR /source/MovieCraft.Server
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Set up the runtime environment
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

# Install dotnet-ef CLI tool for migrations
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

# Generate self-signed SSL certificate using dotnet dev-certs
RUN dotnet dev-certs https -ep /https/localhost.pfx -p ${SSL_CERT_PASSWORD} --trust

# Expose HTTP and HTTPS ports
EXPOSE 80
EXPOSE 443

# Configure HTTPS using environment variables from .env file
ENV ASPNETCORE_URLS="http://+:80;https://+:443"
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/https/localhost.pfx
ENV ASPNETCORE_Kestrel__Certificates__Default__Password=${SSL_CERT_PASSWORD}

# Run the application
ENTRYPOINT ["dotnet", "MovieCraft.Server.dll"]
