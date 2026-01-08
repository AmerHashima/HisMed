# ================================
# Base runtime
# ================================
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# ================================
# Build stage
# ================================
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["src/HIS.Api/HIS.Api.csproj", "src/HIS.Api/"]
COPY ["src/HIS.Application/HIS.Application.csproj", "src/HIS.Application/"]
COPY ["src/HIS.Infrastructure/HIS.Infrastructure.csproj", "src/HIS.Infrastructure/"]
COPY ["src/HIS.Domain/HIS.Domain.csproj", "src/HIS.Domain/"]

RUN dotnet restore "src/HIS.Api/HIS.Api.csproj"

COPY . .

WORKDIR "/src/src/HIS.Api"
RUN dotnet publish "HIS.Api.csproj" \
    -c $BUILD_CONFIGURATION \
    -o /app/publish \
    /p:UseAppHost=false

# ================================
# Final image
# ================================
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

RUN adduser --disabled-password --home /app --gecos '' appuser \
    && chown -R appuser /app

USER appuser

LABEL org.opencontainers.image.source="https://github.com/AmerHashima/HisMed"
LABEL org.opencontainers.image.description="Hospital Information System API (.NET 9)"
LABEL org.opencontainers.image.licenses="MIT"

ENTRYPOINT ["dotnet", "HIS.Api.dll"]
