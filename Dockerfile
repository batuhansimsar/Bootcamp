# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files
COPY Bootcamp.sln .
COPY Bootcamp.Entities/Bootcamp.Entities.csproj Bootcamp.Entities/
COPY Bootcamp.Core/Bootcamp.Core.csproj Bootcamp.Core/
COPY Bootcamp.Repositories/Bootcamp.Repositories.csproj Bootcamp.Repositories/
COPY Bootcamp.Business/Bootcamp.Business.csproj Bootcamp.Business/
COPY Bootcamp.WebAPI/Bootcamp.WebAPI.csproj Bootcamp.WebAPI/

# Restore dependencies
RUN dotnet restore

# Copy all source code
COPY . .

# Build the application
RUN dotnet build -c Release --no-restore

# Publish the application
RUN dotnet publish Bootcamp.WebAPI/Bootcamp.WebAPI.csproj -c Release -o /app/publish --no-build

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Create logs directory
RUN mkdir -p /app/logs

# Copy published app
COPY --from=build /app/publish .

# Expose port
EXPOSE 8080

# Set environment variables
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

# Health check
HEALTHCHECK --interval=30s --timeout=10s --start-period=5s --retries=3 \
    CMD curl -f http://localhost:8080/swagger/index.html || exit 1

# Run the application
ENTRYPOINT ["dotnet", "Bootcamp.WebAPI.dll"]
