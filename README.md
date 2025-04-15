# RRM Web API

A modern .NET Web API solution for RRM (Resource and Relationship Management) system.

## Project Structure

```
RRM.WebApi/
├── src/
│   ├── RRM.WebApi/              # Main Web API project
│   ├── RRM.Application/         # Application layer (business logic)
│   ├── RRM.Domain/             # Domain layer (entities, interfaces)
│   ├── RRM.Infrastructure/     # Infrastructure layer (data access, external services)
│   └── RRM.Shared/             # Shared utilities and common code
├── tests/
│   ├── RRM.WebApi.Tests/       # Unit tests for Web API
│   ├── RRM.Application.Tests/  # Unit tests for Application layer
│   ├── RRM.Domain.Tests/      # Unit tests for Domain layer
│   └── RRM.Infrastructure.Tests/ # Unit tests for Infrastructure layer
└── docs/                       # Project documentation
```

## Prerequisites

- .NET 8.0 SDK or later
- Visual Studio 2022 or Visual Studio Code
- SQL Server (for database)
- Git

## Getting Started

1. Clone the repository:
   ```bash
   git clone [repository-url]
   cd rrm-web-api
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

3. Build the solution:
   ```bash
   dotnet build
   ```

4. Run the application:
   ```bash
   dotnet run --project src/RRM.WebApi
   ```

## Testing

To run all tests:
```bash
dotnet test
```

To run specific test project:
```bash
dotnet test tests/RRM.WebApi.Tests
```

## Architecture

This project follows Clean Architecture principles with the following layers:

- **Web API**: Handles HTTP requests, authentication, and API endpoints
- **Application**: Contains business logic, use cases, and application services
- **Domain**: Contains entities, value objects, and domain interfaces
- **Infrastructure**: Implements data access, external services, and infrastructure concerns
- **Shared**: Contains common utilities and shared code

## Development Guidelines

- Follow SOLID principles
- Write unit tests for all new features
- Use dependency injection for loose coupling
- Follow RESTful API design principles
- Document API endpoints using Swagger/OpenAPI

## API Documentation

API documentation is available at `/swagger` when running the application.

## Contributing

1. Create a feature branch
2. Make your changes
3. Write/update tests
4. Submit a pull request

## License

[Add your license information here]
