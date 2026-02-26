# E-Commerce Web API

### Enterprise-Grade E-Commerce Backend · ASP.NET Core · Clean Onion Architecture

A production-ready, modular RESTful API for e-commerce — featuring JWT-based authentication, the Specification & Repository patterns, Redis-powered caching & shopping baskets, full order lifecycle management, Stripe payment integration, delivery method management, global exception handling with RFC 7807 ProblemDetails, and a strict onion architecture that isolates business logic from all infrastructure concerns.

[📖 Docs](#-api-reference) · [🚀 Quick Start](#-quick-start) · [🐛 Report Bug](https://github.com/Jaser1010/E-Commerce.Web/issues) · [💡 Request Feature](https://github.com/Jaser1010/E-Commerce.Web/issues)

---

## 📑 Table of Contents

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

---

## ⚡ Highlights

| Feature | Description |
|---|---|
| 🏛️ **Clean Onion Architecture** | 8 dedicated projects with strict inward-only dependency flow |
| 🔐 **JWT Authentication** | Full user registration, login, email check & current-user retrieval, powered by ASP.NET Core Identity + JWT Bearer tokens |
| 🛒 **Basket Module (Redis)** | Shopping cart persisted in Redis with create, update & delete support |
| 📦 **Orders Module** | Full order lifecycle — create, retrieve all orders & retrieve a single order by ID, all tied to the authenticated user |
| 🚚 **Delivery Methods** | Configurable shipping options served from `/api/orders/DeliveryMethods` (no auth required) |
| 💳 **Stripe Payment** | Payment intent creation & confirmation via Stripe API |
| ⚡ **Redis Response Caching** | Custom `[RedisCache]` action filter attribute for automatic response caching |
| 🛡️ **Global Exception Handling** | Custom middleware returning RFC 7807 `ProblemDetails` responses |
| 🔍 **Specification Pattern** | Composable, reusable query objects for filters, sorting, pagination & eager loading |
| 📦 **Unit of Work + Generic Repository** | Transactional consistency with a single, type-safe abstraction |
| 🗺️ **AutoMapper + Custom Resolvers** | Entity-to-DTO mapping with `ProductPictureUrlResolver` for dynamic image URLs |
| 📊 **Pagination, Sorting & Filtering** | Server-side pagination via `PaginatedResult<T>`, dynamic sorting & flexible query params |
| 🏷️ **Brands & Types** | Dedicated sub-endpoints to retrieve all product brands and product types |

---

## 🏛 Architecture Overview

This project follows **Onion Architecture** (also known as Clean Architecture), enforcing strict inward-only dependencies:

```
┌────────────────────────────────────────────────────┐
│              Presentation Layer                    │
│   (Controllers, Attributes, Action Filters)        │
├────────────────────────────────────────────────────┤
│               Services Layer                       │
│  (AuthenticationService, OrderService,             │
│   BasketService, CacheService, ProductService)     │
├────────────────────────────────────────────────────┤
│          Services Abstraction Layer                │
│      (Interfaces, DTOs, Service Contracts)         │
├────────────────────────────────────────────────────┤
│             Persistence Layer                      │
│   (EF Core DbContext, Migrations, Repositories,    │
│    Unit of Work, Data Seeding)                     │
├────────────────────────────────────────────────────┤
│               Shared Layer                         │
│     (Common Utilities, Constants, CommonResult)    │
├────────────────────────────────────────────────────┤
│               Domain Layer                         │
│      (Entities, Contracts, No dependencies)        │
└────────────────────────────────────────────────────┘
         ▲ Dependencies flow inward only ▲
```

---

## 📁 Solution Structure

```
E-Commerce.Web.slnx
│
├── E Commerce.Domain/
│   ├── Entities/
│   │   ├── ProductModule/              # Product, Brand, ProductType
│   │   ├── BasketModule/               # CustomerBasket, BasketItem
│   │   ├── OrderModule/                # Order, OrderItem, OrderAddress,
│   │   │                               # DeliveryMethod, OrderStatus,
│   │   │                               # ProductItemOrdered
│   │   └── IdentityModule/             # ApplicationUser
│   └── Contracts/                      # Repository & Unit of Work interfaces
│
├── E Commerce.Persistence/
│   ├── Data/                           # EF Core DbContext, migrations, seed data
│   └── Repositories/                   # Generic & specific repository implementations
│
├── E Commerce.Services Abstraction/
│   ├── IProductService.cs
│   ├── IBasketService.cs
│   ├── IOrderService.cs
│   ├── IAuthenticationService.cs       # ✨ NEW — login, register, email check, current user
│   └── DTOs/                           # Request / Response data transfer objects
│       ├── ProductDTOs/
│       ├── BasketDTOs/
│       ├── OrderDTOs/
│       └── IdentityDTOs/               # ✨ NEW — LoginDTO, RegisterDTO, UserDTO
│
├── E Commerce.Services/
│   ├── ProductService.cs
│   ├── BasketService.cs
│   ├── OrderService.cs                 # ✨ NEW — full order creation & retrieval by ID
│   ├── AuthenticationService.cs        # ✨ NEW — register, login, JWT generation (HMAC-SHA256)
│   ├── CacheService.cs                 # ✨ NEW — Redis response cache abstraction
│   ├── Specifications/                 # Composable query specification objects
│   ├── MappingProfiles/                # AutoMapper profiles
│   └── Exceptions/                     # Custom domain exception types
│
├── E Commerce.Presentation/
│   ├── Controllers/
│   │   ├── ApiBaseController.cs
│   │   ├── ProductsController.cs       # ✨ UPDATED — now requires JWT; adds /brands & /types
│   │   ├── BasketsController.cs        # ✨ UPDATED — added DELETE endpoint
│   │   ├── OrdersController.cs         # ✨ NEW — create, get all, get by ID & delivery methods
│   │   └── AuthenticationController.cs # ✨ NEW — register, login, emailExists, currentUser
│   └── Attributes/
│       └── RedisCacheAttribute.cs      # ✨ NEW — declarative response caching
│
├── E Commerce.Shared/
│   ├── CommonResult/                   # Result<T>, Error (NotFound, Validation, InvalidCredentials)
│   └── (Shared utilities & constants)
│
└── E-Commerce.Web/
    ├── CustomMiddleWares/              # ✨ NEW — global exception handling middleware
    ├── Extensions/                     # Service & middleware registration extensions
    ├── Factories/                      # Problem details factory
    ├── Program.cs
    ├── appsettings.json
    └── wwwroot/                        # Static assets (product images)
```

---

## 🛠️ Tech Stack

| Category | Technology |
|---|---|
| **Framework** | ASP.NET Core 9 Web API |
| **Language** | C# 13 |
| **ORM** | Entity Framework Core 9 |
| **Database** | SQL Server |
| **Cache / Basket Store** | Redis (StackExchange.Redis) |
| **Authentication** | ASP.NET Core Identity + JWT Bearer (HMAC-SHA256) |
| **Object Mapping** | AutoMapper |
| **API Documentation** | Swagger / OpenAPI (Swashbuckle) |
| **Architecture** | Clean Onion Architecture |

---

## 📖 API Reference

### 🔐 Authentication — `/api/authentication`

| Method | Endpoint | Auth | Description |
|---|---|---|---|
| `POST` | `/api/authentication/register` | ❌ | Register a new user; returns `UserDTO` with JWT |
| `POST` | `/api/authentication/login` | ❌ | Login and receive a JWT token |
| `GET` | `/api/authentication/emailExists?email=` | ❌ | Check whether an email address is already registered |
| `GET` | `/api/authentication/CurrentUser` | ✅ JWT | Get the current authenticated user's profile |

**UserDTO response:**

```json
{
  "email": "user@example.com",
  "displayName": "John Doe",
  "token": "<jwt-token>"
}
```

---

### 🛍️ Products — `/api/products`

| Method | Endpoint | Auth | Description |
|---|---|---|---|
| `GET` | `/api/products` | ✅ JWT | Get all products (paginated, sortable, filterable) — cached via Redis |
| `GET` | `/api/products/{id}` | ❌ | Get a single product by ID |
| `GET` | `/api/products/brands` | ❌ | Get all available product brands |
| `GET` | `/api/products/types` | ❌ | Get all available product types |

**Query Parameters for `GET /api/products`:**

| Parameter | Type | Description |
|---|---|---|
| `pageIndex` | `int` | Page number (default: 1) |
| `pageSize` | `int` | Items per page (default: 6) |
| `sort` | `string` | Sort field (e.g. `priceAsc`, `priceDesc`, `nameAsc`) |
| `brandId` | `int?` | Filter by brand ID |
| `typeId` | `int?` | Filter by type ID |
| `search` | `string?` | Search by product name |

---

### 🛒 Baskets — `/api/baskets`

| Method | Endpoint | Auth | Description |
|---|---|---|---|
| `GET` | `/api/baskets?id=` | ❌ | Get a basket by its Redis key |
| `POST` | `/api/baskets` | ❌ | Create or update a basket |
| `DELETE` | `/api/baskets/{id}` | ❌ | Delete a basket by ID |

---

### 📦 Orders — `/api/orders`

| Method | Endpoint | Auth | Description |
|---|---|---|---|
| `POST` | `/api/orders` | ✅ JWT | Create a new order from the authenticated user's basket |
| `GET` | `/api/orders` | ✅ JWT | Get all orders for the authenticated user |
| `GET` | `/api/orders/{id}` | ✅ JWT | Get a specific order by its GUID |
| `GET` | `/api/orders/DeliveryMethods` | ❌ | Get all available delivery methods |

**OrderDTO request body:**

```json
{
  "basketId": "basket-123",
  "deliveryMethodId": 1,
  "shippingAddress": {
    "firstName": "John",
    "lastName": "Doe",
    "street": "123 Main St",
    "city": "Cairo",
    "country": "Egypt"
  }
}
```

---

### 💳 Payments — `/api/payments`

| Method | Endpoint | Auth | Description |
|---|---|---|---|
| `POST` | `/api/payments/{basketId}` | ✅ JWT | Create or update a Stripe payment intent for a basket |

---

## 🚀 Quick Start

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server)
- [Redis](https://redis.io/download)
- [Git](https://git-scm.com/)

### Step-by-Step Setup

**1. Clone the repository**

```bash
git clone https://github.com/Jaser1010/E-Commerce.Web.git
cd E-Commerce.Web
```

**2. Start Redis** (using Docker)

```bash
docker run -d --name redis-ecommerce -p 6379:6379 redis:alpine
```

**3. Configure the application** (`appsettings.Development.json`)

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=ECommerceDB;Trusted_Connection=True;",
    "RedisConnection": "localhost:6379"
  },
  "JWTOptions": {
    "SecretKey": "<your-secret-key-32-chars-min>",
    "Issuer": "ECommerceApp",
    "Audience": "ECommerceUsers"
  },
  "StripeSettings": {
    "SecretKey": "<your-stripe-secret-key>"
  }
}
```

**4. Apply database migrations**

```bash
dotnet ef database update --project "E Commerce.Persistence" --startup-project "E-Commerce.Web"
```

**5. Run the application**

```bash
dotnet run --project "E-Commerce.Web"
```

Swagger UI is available at: `https://localhost:<port>/swagger`

---

## ⚙️ Configuration

| Key | Description |
|---|---|
| `ConnectionStrings:DefaultConnection` | SQL Server connection string |
| `ConnectionStrings:RedisConnection` | Redis server address (e.g. `localhost:6379`) |
| `JWTOptions:SecretKey` | Secret key for signing JWT tokens (min 32 characters) |
| `JWTOptions:Issuer` | JWT token issuer |
| `JWTOptions:Audience` | JWT token audience |
| `StripeSettings:SecretKey` | Stripe secret API key |

---

## 🔬 Design Patterns in Depth

### Specification Pattern

Query objects (e.g. `ProductWithBrandAndTypeSpecification`) encapsulate filtering, sorting, pagination and eager loading:

```csharp
// Composable, reusable, testable
var spec = new ProductWithBrandAndTypeSpecification(queryParams);
var products = await _unitOfWork.Repository<Product>().GetAllWithSpecAsync(spec);
```

### Unit of Work + Generic Repository

A single `IUnitOfWork` gives type-safe access to all repositories and coordinates transactions:

```csharp
var order = await _unitOfWork.Repository<Order>().GetByIdAsync(id);
await _unitOfWork.CompleteAsync();
```

### Result Pattern (CommonResult)

All service methods return `Result<T>` — eliminating exceptions for expected failure scenarios:

```csharp
// In AuthenticationService:
if (user is null)
    return Error.NotFound("User.NotFound", $"No user with email {email} was found");

return new UserDTO(user.Email!, user.DisplayName, token);
```

### Redis Cache Attribute

Decorate any action with `[RedisCache]` to automatically cache the response:

```csharp
[Authorize]
[HttpGet]
[RedisCache]
public async Task<ActionResult<PaginatedResult<ProductDTO>>> GetAllProducts(...)
```

---

## 🛡️ Error Handling

All errors are returned as **RFC 7807 ProblemDetails** via the global exception-handling middleware:

```json
{
  "statusCode": 404,
  "message": "No User With Email user@example.com Was Found"
}
```

**Authentication & validation errors** use the `Result<T>` pattern with typed `Error` codes:

| Error Type | HTTP Status | Example |
|---|---|---|
| `NotFound` | 404 | User or resource not found |
| `InvalidCredentials` | 401 | Wrong email or password |
| `Validation` | 400 | Identity validation errors on register |

---

## 🗺️ Roadmap

- [x] Clean Onion Architecture
- [x] Products & Baskets
- [x] Redis Caching
- [x] Global Exception Handling
- [x] JWT Authentication (register, login, email check, current user)
- [x] Product Brands & Types endpoints
- [x] Full Orders Module (create, get all, get by ID)
- [x] Delivery Methods
- [x] Basket Delete endpoint
- [x] Result Pattern (CommonResult)
- [x] Stripe Payment Integration
- [ ] User address management
- [ ] Real-time order status (SignalR)
- [ ] Admin product management (CRUD)

---

## 🤝 Contributing

Contributions are welcome! Please open an issue first to discuss what you'd like to change.

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

---

## 📄 License

This project is licensed under the MIT License.

---

## 👤 Author

**Jaser Kasim**
[![GitHub](https://img.shields.io/badge/GitHub-Jaser1010-181717?style=flat&logo=github)](https://github.com/Jaser1010)
