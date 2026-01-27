# Base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:10.0-preview AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Build SDK image
FROM mcr.microsoft.com/dotnet/sdk:10.0-preview AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project files only to restore dependencies
COPY ["src/HIS.Api/HIS.Api.csproj", "src/HIS.Api/"]
COPY ["src/HIS.Application/HIS.Application.csproj", "src/HIS.Application/"]
COPY ["src/HIS.Infrastructure/HIS.Infrastructure.csproj", "src/HIS.Infrastructure/"]
COPY ["src/HIS.Domain/HIS.Domain.csproj", "src/HIS.Domain/"]

# Clear NuGet cache & restore
RUN dotnet nuget locals all --clear
RUN dotnet restore "src/HIS.Api/HIS.Api.csproj"

# Copy all source code
COPY . .

# Build the project
RUN dotnet build "src/HIS.Api/HIS.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish
FROM build AS publish
RUN dotnet publish "src/HIS.Api/HIS.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final image
FROM base AS final
WORKDIR /app
RUN apt-get update && apt-get install -y curl && rm -rf /var/lib/apt/lists/*
COPY --from=publish /app/publish .

# Non-root user
RUN adduser --disabled-password --home /app --gecos '' appuser && chown -R appuser /app
USER appuser

# Labels
LABEL org.opencontainers.image.title="HIS API"
LABEL org.opencontainers.image.description="Hospital Information System API built with .NET 10 Preview"
LABEL org.opencontainers.image.version="1.0.0"
LABEL org.opencontainers.image.source="https://github.com/AmerHashima/HisMed.git"
LABEL org.opencontainers.image.licenses="MIT"

# Healthcheck
HEALTHCHECK --interval=30s --timeout=10s --start-period=5s --retries=3 \
    CMD curl -f http://localhost:8080/api/health || exit 1

ENTRYPOINT ["dotnet", "HIS.Api.dll"]
