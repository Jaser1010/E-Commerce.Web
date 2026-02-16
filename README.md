<div align="center">

<img src="https://img.icons8.com/3d-fluency/94/shopping-cart.png" width="80" alt="E-Commerce Logo"/>

# E-Commerce Web API

### Enterprise-Grade E-Commerce Backend · ASP.NET Core · Clean Onion Architecture

<br/>

[![.NET 8](https://img.shields.io/badge/.NET-8.0-512BD4?style=flat-square&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-12-239120?style=flat-square&logo=csharp&logoColor=white)](https://learn.microsoft.com/en-us/dotnet/csharp/)
[![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-Web_API-0078D4?style=flat-square&logo=dotnet&logoColor=white)](https://learn.microsoft.com/en-us/aspnet/core/)
[![EF Core](https://img.shields.io/badge/EF_Core-ORM-68217A?style=flat-square&logo=nuget&logoColor=white)](https://learn.microsoft.com/en-us/ef/core/)
[![Redis](https://img.shields.io/badge/Redis-Caching-DC382D?style=flat-square&logo=redis&logoColor=white)](https://redis.io/)
[![Swagger](https://img.shields.io/badge/Swagger-OpenAPI-85EA2D?style=flat-square&logo=swagger&logoColor=black)](https://swagger.io/)

[![GitHub repo size](https://img.shields.io/github/repo-size/Jaser1010/E-Commerce.Web?style=flat-square&color=blue)](https://github.com/Jaser1010/E-Commerce.Web)
[![GitHub last commit](https://img.shields.io/github/last-commit/Jaser1010/E-Commerce.Web?style=flat-square&color=green)](https://github.com/Jaser1010/E-Commerce.Web/commits/master)
[![GitHub stars](https://img.shields.io/github/stars/Jaser1010/E-Commerce.Web?style=flat-square&color=yellow)](https://github.com/Jaser1010/E-Commerce.Web/stargazers)
[![License: MIT](https://img.shields.io/badge/License-MIT-orange?style=flat-square)](LICENSE)

<br/>

*A production-ready, modular RESTful API for e-commerce — featuring the Specification & Repository patterns,*
*Redis-powered caching & shopping baskets, global exception handling with RFC 7807 ProblemDetails,*
*and a strict onion architecture that isolates business logic from all infrastructure concerns.*

<br/>

[📖 Docs](#-api-reference) · [🚀 Quick Start](#-quick-start) · [🐛 Report Bug](https://github.com/Jaser1010/E-Commerce.Web/issues) · [💡 Request Feature](https://github.com/Jaser1010/E-Commerce.Web/issues)

</div>

---

<br/>

## 📑 Table of Contents

<details>
<summary>Click to expand</summary>

- [Highlights](#-highlights)
- [Architecture Overview](#-architecture-overview)
- [Solution Structure](#-solution-structure)
- [Tech Stack](#-tech-stack)
- [API Reference](#-api-reference)
- [Quick Start](#-quick-start)
- [Configuration](#%EF%B8%8F-configuration)
- [Design Patterns in Depth](#-design-patterns-in-depth)
- [Error Handling](#-error-handling)
- [Roadmap](#-roadmap)
- [Contributing](#-contributing)
- [License](#-license)
- [Author](#-author)

</details>

<br/>

---

## ⚡ Highlights

<table>
<tr>
<td width="50%">

🏛️ **Clean Onion Architecture**
Seven dedicated projects with strict inward-only dependency flow — swap your database, add a gRPC layer, or migrate to microservices without touching a single line of business logic.

</td>
<td width="50%">

🔍 **Specification Pattern**
Composable, reusable query objects that encapsulate filter criteria, sorting, pagination, and eager loading — eliminating query duplication and keeping repositories thin.

</td>
</tr>
<tr>
<td width="50%">

🛒 **Basket Module (Redis-Backed)**
Full shopping cart implementation persisted in Redis with configurable TTL. Supports create, update, retrieve, and delete operations with real-time data access.

</td>
<td width="50%">

⚡ **Redis Response Caching**
Custom `[RedisCache]` action filter attribute that caches API responses with automatic cache-key generation from request path and query parameters.

</td>
</tr>
<tr>
<td width="50%">

🛡️ **Global Exception Handling**
Custom middleware returning RFC 7807 `ProblemDetails` responses with proper HTTP status codes, structured error logging, and consistent error response format.

</td>
<td width="50%">

📦 **Unit of Work + Generic Repository**
A single, type-safe repository abstraction with `UnitOfWork` orchestration — ensuring transactional consistency and reducing boilerplate across all entities.

</td>
</tr>
<tr>
<td width="50%">

🗺️ **AutoMapper + Custom Resolvers**
Entity-to-DTO mapping with a custom `ProductPictureUrlResolver` that dynamically constructs full image URLs based on the server's base address.

</td>
<td width="50%">

📊 **Pagination, Sorting & Filtering**
Server-side pagination with `PaginatedResult<T>`, dynamic sorting via `ProductSortingOptions`, and flexible query parameters through `ProductQueryParams`.

</td>
</tr>
</table>

---

## 🏛 Architecture Overview

The solution implements the **Onion Architecture** (also known as Clean Architecture), where each layer can only reference the layer directly inside it:

```
┌─────────────────────────────────────────────────────────────────────┐
│                                                                     │
│   ┌───────────────────────────────────────────────────────────┐     │
│   │                    PRESENTATION LAYER                     │     │
│   │    API Controllers · Action Filters · RedisCacheAttribute │     │
│   │                 E Commerce.Presentation                   │     │
│   └───────────────────────────┬───────────────────────────────┘     │
│                               │                                     │
│   ┌───────────────────────────▼───────────────────────────────┐     │
│   │                 SERVICE ABSTRACTION LAYER                 │     │
│   │    IProductService · IBasketService · ICacheService       │     │
│   │             E Commerce.Services Abstraction               │     │
│   └───────────────────────────┬───────────────────────────────┘     │
│                               │                                     │
│   ┌───────────────────────────▼───────────────────────────────┐     │
│   │                      SERVICE LAYER                        │     │
│   │  Business Logic · Specifications · MappingProfiles        │     │
│   │  Exceptions · ProductService · BasketService · CacheService│    │
│   │                  E Commerce.Services                      │     │
│   └──────────┬────────────────────────────────┬───────────────┘     │
│              │                                │                     │
│   ┌──────────▼──────────┐          ┌──────────▼──────────┐         │
│   │  PERSISTENCE LAYER  │          │    SHARED LAYER      │         │
│   │  DbContext · Repos  │          │   DTOs · Pagination  │         │
│   │  UnitOfWork · Redis │          │   Query Params       │         │
│   │  Seed · Migrations  │          │   Sorting Options    │         │
│   │ E Commerce.Persist. │          │  E Commerce.Shared   │         │
│   └──────────┬──────────┘          └─────────────────────┘         │
│              │                                                      │
│   ┌──────────▼──────────────────────────────────────────────┐      │
│   │                      DOMAIN LAYER                       │      │
│   │     Core Entities · Contracts · BaseEntity<TKey>        │      │
│   │  IGenericRepository · IUnitOfWork · ISpecifications     │      │
│   │  IBasketRepository · ICacheRepository                   │      │
│   │                   E Commerce.Domain                      │      │
│   └─────────────────────────────────────────────────────────┘      │
│                                                                     │
│   ┌─────────────────────────────────────────────────────────┐      │
│   │                    APPLICATION HOST                      │      │
│   │     DI Container · Middleware Pipeline · Swagger         │      │
│   │     ExceptionHandlerMiddleWare · ApiResponseFactory      │      │
│   │                    E-Commerce.Web                        │      │
│   └─────────────────────────────────────────────────────────┘      │
│                                                                     │
└─────────────────────────────────────────────────────────────────────┘
```

### Layer Responsibilities

| # | Layer | Project | Description |
|:-:|-------|---------|-------------|
| 1 | **Domain** | `E Commerce.Domain` | Core entities (`Product`, `ProductBrand`, `ProductType`, `CustomerBasket`, `BasketItem`), generic `BaseEntity<TKey>`, and all repository/specification contracts. Zero external dependencies. |
| 2 | **Services Abstraction** | `E Commerce.Services Abstraction` | Service interfaces (`IProductService`, `IBasketService`, `ICacheService`) — decoupling business logic from implementation. |
| 3 | **Services** | `E Commerce.Services` | Concrete business logic: `ProductService`, `BasketService`, `CacheService`, specifications (`ProductWithTypeAndBrandSpecification`, `ProductCountSpecifications`), `NotFoundException`, and AutoMapper profiles. |
| 4 | **Persistence** | `E Commerce.Persistence` | EF Core `StoreDbContext`, `GenericRepository<T,TKey>`, `UnitOfWork`, `BasketRepository` (Redis), `CacheRepository` (Redis), `SpecificationsEvaluator`, Fluent API configurations, data seeding. |
| 5 | **Shared** | `E Commerce.Shared` | DTOs (`ProductDTO`, `BrandDTO`, `TypeDTO`, `BasketDTO`, `BasketItemDTO`), `PaginatedResult<T>`, `ProductQueryParams`, `ProductSortingOptions`. |
| 6 | **Presentation** | `E Commerce.Presentation` | API controllers (`ProductsController`, `BasketsController`) and custom action filters (`RedisCacheAttribute`). |
| 7 | **Host** | `E-Commerce.Web` | Application entry point — configures DI, `ExceptionHandlerMiddleWare`, `ApiResponseFactory`, Swagger, auto migration & seeding via extension methods. |

> **Why this matters:** Business rules live in the innermost layers. Infrastructure details (databases, Redis, HTTP) live in the outer layers. You can replace SQL Server with PostgreSQL, swap Redis for Memcached, or add a gRPC gateway alongside REST — without modifying any business logic.

---

## 📁 Solution Structure

```
E-Commerce.Web.slnx
│
├── 🏗️ E Commerce.Domain/                     # Domain Layer — Zero Dependencies
│   ├── Entities/
│   │   ├── BaseEntity.cs                     # Generic base with typed Id
│   │   ├── ProductModule/
│   │   │   ├── Product.cs                    # Core product entity
│   │   │   ├── ProductBrand.cs               # Brand lookup entity
│   │   │   └── ProductType.cs                # Type/category entity
│   │   └── BasketModule/
│   │       ├── CustomerBasket.cs             # Shopping basket (Redis-persisted)
│   │       └── BasketItem.cs                 # Individual basket line item
│   └── Contracts/
│       ├── IGenericRepository.cs             # Generic CRUD + specification support
│       ├── ISpecifications.cs                # Specification interface with Include/OrderBy/Pagination
│       ├── IUnitOfWork.cs                    # Unit of Work for transactional consistency
│       ├── IBasketRepository.cs              # Basket persistence contract (Redis)
│       ├── ICacheRepository.cs               # Caching contract (Redis)
│       └── IDataInitializer.cs               # Data seeding contract
│
├── 📋 E Commerce.Services Abstraction/        # Service Contracts
│   ├── IProductService.cs                    # Product operations interface
│   ├── IBasketService.cs                     # Basket operations interface
│   └── ICacheService.cs                      # Caching operations interface
│
├── ⚙️ E Commerce.Services/                    # Business Logic
│   ├── ProductService.cs                     # Product CRUD + pagination
│   ├── BasketService.cs                      # Basket CRUD operations
│   ├── CacheService.cs                       # Cache get/set with TTL
│   ├── ServicesAssemblyReference.cs          # AutoMapper assembly marker
│   ├── Exceptions/
│   │   └── NotFoundException.cs              # Custom 404 exception
│   ├── MappingProfiles/
│   │   ├── ProductProfile.cs                 # Product → ProductDTO mapping
│   │   ├── ProductPictureUrlResolver.cs      # Resolves relative → absolute image URLs
│   │   └── BasketProfile.cs                  # CustomerBasket → BasketDTO mapping
│   └── Specifications/
│       ├── BaseSpecifications.cs             # Abstract spec with Criteria, Includes, Ordering, Pagination
│       ├── ProductWithTypeAndBrandSpecification.cs  # Products + eager-loaded Brand & Type
│       ├── ProductCountSpecifications.cs     # Count filter for pagination metadata
│       └── ProductSpecificationsHelper.cs    # Reusable filter/sort expression builder
│
├── 🗃️ E Commerce.Persistence/                 # Infrastructure / Data Access
│   ├── SpecificationsEvaluator.cs            # Translates specs → EF Core IQueryable
│   ├── Data/
│   │   ├── DbContexts/
│   │   │   └── StoreDbContext.cs             # EF Core context with Fluent API
│   │   ├── Configurations/
│   │   │   ├── ProductConfiguration.cs       # Product table schema
│   │   │   ├── ProductBrandConfiguration.cs  # Brand table schema
│   │   │   └── ProductTypeConfiguration.cs   # Type table schema
│   │   ├── DataSeed/
│   │   │   ├── DataInitializer.cs            # Seeding orchestrator
│   │   │   └── JSONFiles/
│   │   │       ├── products.json             # 13 seed products
│   │   │       ├── brands.json               # Seed brands
│   │   │       └── types.json                # Seed types
│   │   └── Migrations/                       # EF Core migration history
│   └── Repositories/
│       ├── GenericRepository.cs              # Generic repo with spec evaluation
│       ├── UnitOfWork.cs                     # Coordinates repository + SaveChanges
│       ├── BasketRepository.cs               # Redis-backed basket persistence
│       └── CacheRepository.cs                # Redis-backed cache key/value store
│
├── 🌐 E Commerce.Presentation/                # API Layer
│   ├── Controllers/
│   │   ├── ProductsController.cs             # Products CRUD + pagination
│   │   └── BasketsController.cs              # Shopping basket CRUD
│   └── Attributes/
│       └── RedisCacheAttribute.cs            # Custom action filter for response caching
│
├── 📦 E Commerce.Shared/                      # Cross-Cutting DTOs & Query Models
│   ├── DTOs/
│   │   ├── ProductDTOs/
│   │   │   ├── ProductDTO.cs                 # Product response model
│   │   │   ├── BrandDTO.cs                   # Brand response model
│   │   │   └── TypeDTO.cs                    # Type response model
│   │   └── BasketDTOs/
│   │       ├── BasketDTO.cs                  # Basket response model
│   │       └── BasketItemDTO.cs              # Basket item response model
│   ├── PaginatedResult.cs                    # Generic pagination envelope
│   ├── ProductQueryParams.cs                 # Sort/filter/page query model
│   └── ProductSortingOptions.cs              # Enum: NameAsc, NameDesc, PriceAsc, PriceDesc
│
├── 🚀 E-Commerce.Web/                         # Application Host
│   ├── Program.cs                            # Entry point — DI, Redis, AutoMapper, Swagger
│   ├── CustomMiddleWares/
│   │   └── ExceptionHandlerMiddleWare.cs     # Global exception handler (ProblemDetails)
│   ├── Extensions/
│   │   └── WebApplicationRegistration.cs     # Auto-migration & data seeding extensions
│   ├── Factories/
│   │   └── ApiResponseFactory.cs             # Validation error response factory
│   ├── appsettings.json                      # Base configuration
│   ├── appsettings.Development.json          # Dev-specific config (connection strings)
│   ├── Properties/
│   │   └── launchSettings.json               # Launch profiles
│   └── wwwroot/images/products/              # 13 product images
│
├── E-Commerce.Web.slnx                        # Solution file
├── .gitignore
└── .gitattributes
```

---

## 🛠️ Tech Stack

| Category | Technology | Purpose |
|:---------|:-----------|:--------|
| **Runtime** | [.NET 8.0+](https://dotnet.microsoft.com/) | Long-term support runtime |
| **Framework** | [ASP.NET Core Web API](https://learn.microsoft.com/en-us/aspnet/core/) | HTTP pipeline, routing & middleware |
| **Language** | [C# 12](https://learn.microsoft.com/en-us/dotnet/csharp/) | Primary language |
| **ORM** | [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/) | Database access, migrations & Fluent API |
| **Database** | [SQL Server](https://www.microsoft.com/en-us/sql-server/) | Primary relational data store |
| **Cache / NoSQL** | [Redis](https://redis.io/) (via [StackExchange.Redis](https://github.com/StackExchange/StackExchange.Redis)) | Basket persistence & API response caching |
| **Mapping** | [AutoMapper](https://automapper.org/) | Object-to-object mapping with custom resolvers |
| **API Docs** | [Swashbuckle (Swagger)](https://github.com/domaindrivendev/Swashbuckle.AspNetCore) | Interactive OpenAPI documentation |
| **Error Handling** | [RFC 7807 ProblemDetails](https://tools.ietf.org/html/rfc7807) | Standardized error response format |
| **Patterns** | Repository, Specification, Unit of Work, DI, DTO | Enterprise design patterns |
| **Architecture** | Clean Onion Architecture | Layered separation of concerns |

---

## 📖 API Reference

### Products

| Method | Endpoint | Description | Cache |
|:------:|:---------|:------------|:-----:|
| `GET` | `/api/products` | Paginated product list with sorting & filtering | ✅ Redis |
| `GET` | `/api/products/{id}` | Single product by ID | — |
| `GET` | `/api/products/brands` | All product brands | — |
| `GET` | `/api/products/types` | All product types | — |

#### Query Parameters (`GET /api/products`)

| Parameter | Type | Description | Example |
|:----------|:-----|:------------|:--------|
| `sort` | `string` | Sorting option: `NameAsc`, `NameDesc`, `PriceAsc`, `PriceDesc` | `?sort=PriceAsc` |
| `brandId` | `int?` | Filter by brand ID | `?brandId=1` |
| `typeId` | `int?` | Filter by type ID | `?typeId=2` |
| `pageIndex` | `int` | Page number (1-based) | `?pageIndex=1` |
| `pageSize` | `int` | Items per page | `?pageSize=5` |
| `search` | `string?` | Search by product name | `?search=jacket` |

### Baskets

| Method | Endpoint | Description |
|:------:|:---------|:------------|
| `GET` | `/api/baskets?id={basketId}` | Retrieve a customer basket by ID |
| `POST` | `/api/baskets` | Create or update a customer basket |
| `DELETE` | `/api/baskets/{id}` | Delete a customer basket |

<details>
<summary><b>📋 Example: GET /api/products — Paginated Response</b></summary>

```json
{
  "pageIndex": 1,
  "pageSize": 5,
  "count": 13,
  "data": [
    {
      "id": 1,
      "name": "Classic White T-Shirt",
      "description": "A timeless white cotton t-shirt for everyday wear",
      "pictureUrl": "https://localhost:5001/images/products/ClassicWhiteTShirt.jpeg",
      "price": 29.99,
      "productBrand": "Nike",
      "productType": "Tops"
    }
  ]
}
```

</details>

<details>
<summary><b>📋 Example: GET /api/products/{id} — Single Product</b></summary>

```json
{
  "id": 5,
  "name": "Denim Jacket",
  "description": "Classic denim jacket with a modern fit",
  "pictureUrl": "https://localhost:5001/images/products/DenimJacket.jpg",
  "price": 89.99,
  "productBrand": "Levi's",
  "productType": "Outerwear"
}
```

</details>

<details>
<summary><b>📋 Example: POST /api/baskets — Create/Update Basket</b></summary>

**Request Body:**

```json
{
  "id": "basket-abc123",
  "items": [
    {
      "id": 1,
      "productName": "Classic White T-Shirt",
      "pictureUrl": "images/products/ClassicWhiteTShirt.jpeg",
      "price": 29.99,
      "quantity": 2
    },
    {
      "id": 5,
      "productName": "Denim Jacket",
      "pictureUrl": "images/products/DenimJacket.jpg",
      "price": 89.99,
      "quantity": 1
    }
  ]
}
```

**Response:** `200 OK` — Returns the saved basket.

</details>

<details>
<summary><b>📋 Example: Error Response (404 Not Found)</b></summary>

```json
{
  "title": "Error While Processing HTTP Request",
  "detail": "Product with id 999 not found.",
  "status": 404,
  "instance": "/api/products/999"
}
```

All errors follow the [RFC 7807 ProblemDetails](https://tools.ietf.org/html/rfc7807) specification.

</details>

### Entity Models

<details>
<summary><b>🏗️ Product Entity</b></summary>

```csharp
public class Product : BaseEntity<int>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string PictureUrl { get; set; }
    public decimal Price { get; set; }

    // Navigation Properties
    public int BrandId { get; set; }
    public ProductBrand ProductBrand { get; set; }

    public int TypeId { get; set; }
    public ProductType ProductType { get; set; }
}
```

</details>

<details>
<summary><b>🛒 CustomerBasket Entity (Redis)</b></summary>

```csharp
public class CustomerBasket
{
    public string Id { get; set; }               // GUID created client-side
    public ICollection<BasketItem> Items { get; set; } = [];
}

public class BasketItem
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public string PictureUrl { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
```

</details>

---

## 🚀 Quick Start

### Prerequisites

| Requirement | Minimum Version | Download |
|:------------|:----------------|:---------|
| **.NET SDK** | 8.0 | [dotnet.microsoft.com](https://dotnet.microsoft.com/download) |
| **SQL Server** | Any recent | [SQL Server Downloads](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) |
| **Redis Server** | 6.0+ | [redis.io](https://redis.io/download) or [Docker](https://hub.docker.com/_/redis) |
| **Git** | 2.x | [git-scm.com](https://git-scm.com/downloads) |

### Step-by-Step Setup

```bash
# 1. Clone the repository
git clone https://github.com/Jaser1010/E-Commerce.Web.git
cd E-Commerce.Web

# 2. (Optional) Switch to the advanced branch with Basket + Caching features
git checkout B1

# 3. Start Redis (if not already running)
#    Option A: Docker
docker run -d -p 6379:6379 --name redis redis
#    Option B: Windows — install via Memurai or WSL

# 4. Restore NuGet packages
dotnet restore

# 5. Update connection strings in appsettings.Development.json if needed

# 6. Run the application (auto-migrates DB + seeds data on startup)
dotnet run --project E-Commerce.Web
```

### Verify Installation

Once running, open your browser:

| URL | What You'll See |
|:----|:----------------|
| `https://localhost:5001/swagger` | 📜 **Swagger UI** — interactive API explorer |
| `https://localhost:5001/api/products` | 📦 **JSON** — paginated list of 13 seeded products |
| `https://localhost:5001/api/products/brands` | 🏷️ **JSON** — available product brands |

> 💡 **Tip:** The database is automatically migrated and seeded with sample products, brands, and types on first startup — no manual SQL scripts needed.

---

## ⚙️ Configuration

All settings are managed through `appsettings.Development.json`:

```jsonc
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=ECommerceDb;Trusted_Connection=True;TrustServerCertificate=True;",
    "RedisConnection": "localhost"
  },
  "ApiUrl": "https://localhost:5001",
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

| Key | Description |
|:----|:------------|
| `ConnectionStrings:DefaultConnection` | SQL Server connection string for EF Core |
| `ConnectionStrings:RedisConnection` | Redis server address for basket storage & response caching |
| `ApiUrl` | Base URL used by `ProductPictureUrlResolver` to construct full image URLs |
| `Logging:LogLevel` | Minimum log verbosity levels |

---

## 🔬 Design Patterns in Depth

<details>
<summary><b>📦 Generic Repository + Unit of Work</b></summary>

The `IGenericRepository<TEntity, TKey>` interface abstracts all data access, while `IUnitOfWork` coordinates changes across multiple repositories:

```csharp
// Repository — handles individual entity operations
public interface IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, TKey> specifications);
    Task<TEntity?> GetByIdAsync(TKey id);
    Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, TKey> specifications);
    Task AddAsync(TEntity entity);
    void Update(TEntity entity);
    void Remove(TEntity entity);
    Task<int> CountAsync(ISpecifications<TEntity, TKey> specifications);
}

// Unit of Work — coordinates repositories + transactional SaveChanges
public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
    IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
        where TEntity : BaseEntity<TKey>;
}
```

**Why?** One generic repository serves all entities. The Unit of Work ensures all changes within a business transaction are committed or rolled back together.

</details>

<details>
<summary><b>🔍 Specification Pattern</b></summary>

Specifications encapsulate query logic into reusable, composable objects with full support for filtering, sorting, pagination, and eager loading:

```csharp
public interface ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
{
    ICollection<Expression<Func<TEntity, object>>> IncludeExpressions { get; }
    Expression<Func<TEntity, bool>> Criteria { get; }
    Expression<Func<TEntity, object>>? OrderBy { get; }
    Expression<Func<TEntity, object>>? OrderByDescending { get; }
    int Skip { get; }
    int Take { get; }
    bool IsPaginated { get; }
}

// Usage example:
var spec = new ProductWithTypeAndBrandSpecification(queryParams);
var products = await _unitOfWork.GetRepository<Product, int>().GetAllAsync(spec);
```

**Benefits:**

- ✅ Queries are unit-testable (they're just classes)
- ✅ Filtering, sorting, includes, and pagination are composable
- ✅ `SpecificationsEvaluator` translates specs → EF Core `IQueryable` expressions
- ✅ Repository stays generic — no method explosion

</details>

<details>
<summary><b>⚡ Redis Response Caching (Custom Action Filter)</b></summary>

The `[RedisCache]` attribute automatically caches API responses using cache keys generated from the request path + sorted query parameters:

```csharp
[HttpGet]
[RedisCache]  // Caches response for 5 min (configurable)
public async Task<ActionResult<PaginatedResult<ProductDTO>>> GetAllProducts(
    [FromQuery] ProductQueryParams queryParams) { ... }
```

**How it works:**

1. Intercepts the request before the action executes
2. Generates a cache key: `/api/products|brandId-1|pageIndex-1|pageSize-5|sort-PriceAsc`
3. If cache hit → returns cached `200 OK` JSON immediately (skips controller)
4. If cache miss → executes the action, then stores the response in Redis with a configurable TTL

</details>

<details>
<summary><b>🗺️ PictureUrlResolver</b></summary>

A custom AutoMapper `IValueResolver` that transforms relative `PictureUrl` paths into fully qualified URLs:

```
Input:   "images/products/DenimJacket.jpg"
Output:  "https://localhost:5001/images/products/DenimJacket.jpg"
```

Configured via the `ApiUrl` setting in `appsettings.json`, ensuring URLs automatically adapt per environment (dev, staging, production).

</details>

---

## 🛡️ Error Handling

The API implements a **global exception handling middleware** that catches all unhandled exceptions and returns structured RFC 7807 `ProblemDetails` responses:

| Scenario | Status Code | Handling |
|:---------|:-----------:|:---------|
| Custom `NotFoundException` | `404` | Returned when entity lookup fails |
| Unhandled exceptions | `500` | Logged via `ILogger`, generic error returned |
| Invalid endpoint (route not found) | `404` | Caught by `HandleNotFoundEndPointAsync` |
| Validation errors | `400` | Handled by `ApiResponseFactory.GenerateApiValidationResponse` |

All error responses follow this structure:

```json
{
  "title": "Error While Processing HTTP Request",
  "detail": "Specific error message here",
  "status": 404,
  "instance": "/api/products/999"
}
```

---

## 🗺️ Roadmap

> The project is actively evolving. Here's what's been built and what's next:

#### ✅ Completed

- [x] **Product Module** — Paginated CRUD with brand & type navigation
- [x] **Basket Module** — Redis-backed shopping cart (create/update/get/delete)
- [x] **Clean Onion Architecture** — 7-project solution with strict layer isolation
- [x] **Generic Repository + Unit of Work** — Typed repository abstraction with transactional coordination
- [x] **Specification Pattern** — Composable queries with filtering, sorting & eager loading
- [x] **Server-side Pagination** — `PaginatedResult<T>` with total count & configurable page size
- [x] **Redis Response Caching** — Custom `[RedisCache]` action filter attribute
- [x] **AutoMapper** with custom `ProductPictureUrlResolver`
- [x] **Global Exception Middleware** — RFC 7807 ProblemDetails responses
- [x] **Validation Error Factory** — Consistent model validation error format
- [x] **Automatic DB Migration & Data Seeding** — Via extension methods on startup
- [x] **Swagger / OpenAPI** — Interactive API documentation
- [x] **Static File Serving** — 13 product images served via `wwwroot`

#### 🔜 Coming Soon

- [ ] 📋 **Order Module** — Checkout flow, order history & tracking
- [ ] 🔐 **Identity & Authentication** — JWT tokens, user registration, role-based auth
- [ ] 💳 **Payment Integration** — Stripe payment intents & webhook handling
- [ ] 📧 **Email Notifications** — Order confirmation & shipping updates
- [ ] 🐳 **Docker Support** — Containerized deployment with `docker-compose`
- [ ] 🧪 **Testing Suite** — Unit tests, integration tests & API endpoint tests
- [ ] 📊 **Structured Logging** — Serilog with Seq or Elasticsearch sink

---

## 🤝 Contributing

Contributions make the open-source community an incredible place to learn, inspire, and create. Every contribution is **greatly appreciated**.

<details>
<summary><b>How to contribute</b></summary>

1. **Fork** the repository
2. **Create** a feature branch

   ```bash
   git checkout -b feature/amazing-feature
   ```

3. **Commit** your changes

   ```bash
   git commit -m "feat: add amazing feature"
   ```

4. **Push** to your fork

   ```bash
   git push origin feature/amazing-feature
   ```

5. **Open** a Pull Request

</details>

> **Note:** This project is part of ongoing coursework development. Suggestions, bug reports, and pull requests are always welcome!

---

## 📄 License

Distributed under the **MIT License** — see the [LICENSE](LICENSE) file for details.

---

## 👤 Author

<table>
<tr>
<td align="center">
<a href="https://github.com/Jaser1010"><b>Jaser Kasim</b></a>
<br/><sub>Software Engineer</sub>
<br/><br/>
<a href="https://github.com/Jaser1010"><img src="https://img.shields.io/badge/GitHub-100000?style=flat-square&logo=github&logoColor=white" alt="GitHub"/></a>
<a href="https://www.linkedin.com/in/jaser-kasim-j1k2/"><img src="https://img.shields.io/badge/LinkedIn-0077B5?style=flat-square&logo=linkedin&logoColor=white" alt="LinkedIn"/></a>
</td>
</tr>
</table>

---

<div align="center">

**If you found this project helpful, consider giving it a ⭐**

<br/>

Built with ❤️ using **C#** and **ASP.NET Core**

<sub>© 2025 Jaser. All rights reserved.</sub>

</div>
