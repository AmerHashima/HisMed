# Use the official .NET 9 runtime as the base image for final stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Use the official .NET 9 SDK for build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project files and restore dependencies
# Copy all project files to ensure proper dependency resolution
COPY ["src/HIS.Api/HIS.Api.csproj", "src/HIS.Api/"]
COPY ["src/HIS.Application/HIS.Application.csproj", "src/HIS.Application/"]
COPY ["src/HIS.Infrastructure/HIS.Infrastructure.csproj", "src/HIS.Infrastructure/"]
COPY ["src/HIS.Domain/HIS.Domain.csproj", "src/HIS.Domain/"]

# Restore dependencies for the main API project
RUN dotnet restore "src/HIS.Api/HIS.Api.csproj"

# Copy the entire source code
COPY . .

# Build the application
WORKDIR "/src/src/HIS.Api"
RUN dotnet build "HIS.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the application
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "HIS.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final stage/image
FROM base AS final
WORKDIR /app

# Install curl for health checks
RUN apt-get update && apt-get install -y curl && rm -rf /var/lib/apt/lists/*

# Copy published application
COPY --from=publish /app/publish .

# Create a non-root user for security
RUN adduser --disabled-password --home /app --gecos '' appuser && chown -R appuser /app
USER appuser

# Add metadata labels
LABEL org.opencontainers.image.title="HIS API"
LABEL org.opencontainers.image.description="Hospital Information System API built with .NET 9"
LABEL org.opencontainers.image.version="1.0.0"
LABEL org.opencontainers.image.source="https://github.com/AmerHashima/HisMed.git"
LABEL org.opencontainers.image.licenses="MIT"

# Health check
HEALTHCHECK --interval=30s --timeout=10s --start-period=5s --retries=3 \
    CMD curl -f http://localhost:8080/api/health || exit 1

ENTRYPOINT ["dotnet", "HIS.Api.dll"]