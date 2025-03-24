# Syntrice EShop 

A work-in-progress ecommerce system example, consisting of backend API (WIP) and frontend web app (not yet developed).
Follows test-driven-development on the Service and Controller layers.

## Features

- [x] Password hashing + salting
- [x] Configurable JWT Authentication with cookie support
- [x] Configurable refresh token system
- [ ] Product listing system (WIP)
- [ ] Shopping cart system (WIP)
- [ ] Order system (WIP)
- [ ] Front-end interface (WIP)

## Technologies

- Languages: C# / .NET
- Frameworks: ASP.NET Core
- Data Management: SQLite, Entity Framework Core
- Testing: NUnit 3, NSubstitute, Shouldly
- General: Dependency Injection, Layered Architecture, HTTP Request Handling, Repository-Service-Controller Pattern, Generics, Test Driven Development, Data Transfer Objects (DTO), DTO Mapping, Password Hashing + Salting, JWT Authentication, Cookies, Refresh Tokens

## Database Schema

![syntrice-e-shop-er drawio](https://github.com/user-attachments/assets/a5864a83-cfc0-4145-8d79-19f624f48946)

## Setup
To build the project, you will need a copy of the [.NET 9.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0).

```bash
# Clone the repository
git clone "https://github.com/Syntrice/syntrice-e-shop.git"

# Navigate to repository root
cd "./syntrice-e-shop"

# Restore dependencies
dotnet restore

# Build Project
dotnet build "./SyntriceEShop.sln"
```

The project is configured to use a local SQLite database with seeded data when running in development mode. To enable this, run with the environment variable `ASPNETCORE_ENVIRONMENT` set to `Development`. Production mode is not yet configured.

## Licence

This project is licenced under MIT


