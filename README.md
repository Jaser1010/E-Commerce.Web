# E-Commerce Web API (ASP.NET Core)

A Web API for an e-commerce application built with **ASP.NET Core** using a **Clean Onion Architecture**. The solution organizes code into layers to enforce separation of concerns: Domain, Application (Services), Infrastructure (Persistence), Presentation (API), and Shared. Each layer depends only on the layer immediately inside it, making the codebase modular, testable and maintainable.

## Architecture

The project follows an onion architecture pattern:

- **Domain Layer**: Defines core business entities and interfaces (e.g., `Product`, `ProductBrand`, `ProductType`) without implementation details.
- **Application Layer**: Contains service abstractions and business logic. Services operate on domain entities through interfaces and are decoupled from infrastructure.
- **Infrastructure Layer (Persistence)**: Implements repositories and data access using `DbContext`, caching and other external concerns.
- **Presentation Layer**: Exposes RESTful API controllers for client interaction.
- **Shared Layer**: Contains Data Transfer Objects (DTOs) and common utilities.

This layering enables replacing the database or front end without changing business logic.

## Product Module

The API currently includes a **Product** module with endpoints to manage products and lookup lists:

- `GET /api/products` – Retrieve a paginated list of products with brand and type information.
- `GET /api/products/{id}` – Retrieve details for a single product.
- `GET /api/products/brands` – Get all product brands.
- `GET /api/products/types` – Get all product types.

### Product Entity

A `Product` has the following properties:

- `Id` (int): Unique identifier
- `Name` (string): Product name
- `Description` (string): Detailed description
- `PictureUrl` (string): URL of the product image
- `Price` (decimal): Product price
- `BrandId` (int): Foreign key to `ProductBrand`
- `TypeId` (int): Foreign key to `ProductType`

`ProductBrand` and `ProductType` entities each have `Id` and `Name` fields.

### Generic Repository & Specification Pattern

Data access is implemented using a **generic repository** that provides common operations: `Add`, `Update`, `Remove`, `GetById`, and `GetAll`. For more complex queries, the API uses the **Specification pattern** to encapsulate query criteria and includes, making it easy to compose filters and eager load related entities. For example, fetching a product with its brand and type uses a specification that includes related entities.

### Picture URL Resolver

The API uses a custom `PictureUrlResolver` with AutoMapper to construct full image URLs when mapping entities to DTOs.

### Data Seeding

Initial product, brand and type data are seeded into the database. The seed data is defined in separate files and loaded during application startup.

## Running the API

Prerequisites:

- [.NET SDK](https://dotnet.microsoft.com/download) (version 8 or above)
- SQL Server or SQLite (configured in `appsettings.json`)

Steps to run locally:

1. Clone the repository:

```bash
git clone https://github.com/Jaser1010/E-Commerce.Web.git
cd E-Commerce.Web
```

2. Update database connection strings in `appsettings.json` if needed.

3. Restore dependencies and run migrations:

```bash
dotnet restore
dotnet ef database update
```

4. Run the API:

```bash
dotnet run --project E-Commerce.Web
```

The API will be available at `https://localhost:5001` by default.

API documentation is available via Swagger at `/swagger`.

## Contributing

This project is a work in progress aligned with coursework. Contributions and suggestions are welcome. Feel free to open issues or pull requests.
