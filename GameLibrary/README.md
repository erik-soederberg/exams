# GameLibrary API

A REST API built with ASP.NET Core (.NET 9) and Entity Framework Core for managing a game library with games, genres and platforms.

## Technologies
- .NET 9
- Entity Framework Core
- PostgreSQL
- ASP.NET Core Identity
- JWT Authentication
- Swagger/OpenAPI

## Getting Started

### Requirements
- .NET 9 SDK
- Docker

### Start the database
```bash
docker run --name gamelibrary-db \
  -e POSTGRES_USER=postgres \
  -e POSTGRES_PASSWORD=postgres \
  -e POSTGRES_DB=gamelibrary \
  -p 5432:5432 \
  -d postgres
```

### Run migrations and seed the database
```bash
dotnet ef database update --project GameLibraryData --startup-project GameLibraryApi
```

### Start the API
```bash
cd GameLibraryApi
dotnet run
```

### Swagger
Once the API is running, Swagger is available at:
```
http://localhost:5134/swagger
```

## Authentication
Some endpoints require a JWT token. Register a user and log in to get a token:

**Register:**
```
POST /api/Auth/register
```

**Login:**
```
POST /api/Auth/login
```

Paste the token in Swagger under "Authorize" or in Postman under "Bearer Token".

## Tests
```bash
dotnet test GameLibrary.Tests/GameLibrary.Tests.csproj
```

## Postman
Import `GameLibrary.postman_collection.json` into Postman to test all endpoints.