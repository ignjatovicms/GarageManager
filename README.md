# GarageManager

GarageManager is a backend application for managing automotive repair shop operations.

The project was designed around real‑world garage workflows such as customers, vehicles, service orders, parts, services, pricing, warranties and monthly business statistics.

---

## Features

- Customer management  
- Vehicle management  
- Service order management  
- Parts and services within service orders  
- Manual pricing for parts and services  
- Quantity and units (piece, liter, hour, service)  
- Warranty tracking  
- Vehicle mileage tracking  
- Vehicle condition and notes  
- Arrival and departure time tracking  
- Automatic service duration calculation  
- Automatic parts, services and total price calculation  
- Monthly revenue and order statistics  
- Optional customer and vehicle for orders  
- Part sales without vehicle service  
- REST API with Swagger documentation  
- Validation and HTTP error handling  
- Automated tests for core business logic  

---

## Architecture

The application follows a layered structure:

- **Controllers** – handle HTTP requests and responses  
- **Services** – contain business logic  
- **DTOs** – control data exchanged through the API  
- **Models** – represent domain entities  
- **DbContext** – handles database access through Entity Framework Core  
- **Enums** – define order types, item types, units and statuses  

---

## Live Demo

Explore the deployed API through Swagger:

**[Open Swagger UI](http://garagemanager.runasp.net/swagger)**

## Main Entities

### Customer
Represents a garage customer and can have multiple vehicles and service orders.

### Car
Represents a vehicle belonging to a customer.

### ServiceOrder
Represents a visit/order in the garage.  
An order can contain:

- Customer  
- Vehicle  
- Mileage  
- Vehicle condition  
- Arrival/departure time  
- Notes  
- Parts  
- Services  
- Order status  

### ServiceOrderItem
Represents either a part or a service.  
Examples:

- Motul 5W‑30 engine oil  
- Brembo brake disc  
- Mann oil filter  
- Oil change  
- Polishing  

Each item supports quantity, unit price, warranty information and notes.

---

## Business Logic

The application calculates:

- Parts total  
- Services total  
- Total order price  
- Vehicle stay duration  

Example:

```text
Parts:
5 × 1,500 RSD = 7,500 RSD
1 × 1,200 RSD = 1,200 RSD

Services:
1 × 1,500 RSD = 1,500 RSD

Total:
10,200 RSD
```

## Monthly Statistics

The application provides monthly business statistics, including:

- Total orders
- Completed orders
- Vehicle orders
- Part sales
- Parts revenue
- Services revenue
- Total revenue

## Technologies

| Category | Tools / Frameworks |
|---|---|
| Language | C# |
| Framework | .NET, ASP.NET Core Web API |
| ORM | Entity Framework Core |
| Database | SQL Server |
| API Documentation | Swagger / OpenAPI |
| Testing | xUnit, EF Core InMemory |
| Version Control | Git / GitHub |

## Testing

The project includes automated tests for core business logic using xUnit and EF Core InMemory.

Current tests cover:

- Order price calculation
- Parts and services revenue calculation
- Service duration calculation
- Monthly statistics
- Edge case when a vehicle has no departure time

## API

The API can be explored through Swagger.

### Main Resources

```text
/api/Customers
/api/Cars
/api/ServiceOrders
/api/ServiceOrders/{id}/items
/api/ServiceOrders/statistics/{year}/{month}
```
Project Status

Backend v1 is complete.

The next phase of the project is the development of a frontend application focused on providing a simple and practical interface for everyday garage use.

Built with ❤️ for garage owners and mechanics.
