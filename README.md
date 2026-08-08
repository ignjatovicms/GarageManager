# GarageManager

GarageManager is a RESTful Web API for managing garage operations, built with ASP.NET Core and C#.

The project is being developed as a personal portfolio project to practice building a layered .NET application, working with relational databases, and designing REST APIs.

## Technologies

- C#
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- REST API
- Swagger / OpenAPI
- Docker
- Git / GitHub

## Architecture

The application follows a layered structure that separates HTTP request handling, business logic, and data access.

```text
Client
   ↓
Controller
   ↓
Service
   ↓
Entity Framework Core
   ↓
SQL Server
```


- **Controller** – handles HTTP requests and responses
- **Service** – contains business logic, decoupled via `ICarService`
- **DTOs** – shape data exchanged with the client, separate from the database entities
- **EF Core** – handles data persistence and migrations

## Endpoints

| Method | Route              | Description                  |
|--------|---------------------|-------------------------------|
| GET    | `/api/cars`          | Get all cars                  |
| GET    | `/api/cars/{id}`     | Get a car by ID               |
| GET    | `/api/cars/search`   | Search cars by brand/model    |
| POST   | `/api/cars`          | Create a new car              |
| PUT    | `/api/cars/{id}`     | Update an existing car        |
| DELETE | `/api/cars/{id}`     | Delete a car                  |

## Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download)
- SQL Server (LocalDB or full instance)

### Setup

```bash
# Clone the repository
git clone https://github.com/ignjatovicms/GarageManager.git
cd GarageManager

# Update the connection string in appsettings.json to match your SQL Server instance

# Apply migrations
dotnet ef database update

# Run the application
dotnet run
```

The API will be available at `https://localhost:5162/swagger` for interactive documentation
(port may differ depending on your local configuration — check the console output when running `dotnet run`).

## Status

🚧 Work in progress — this project is under active development as part of my portfolio.
