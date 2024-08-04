# LibraryNiceBooks-API

LibraryNiceBooks-API is a .NET-based API designed to manage and provide data about books, including their ID, title, author, price, published date, and photo URL. This API uses modern technologies and best practices to ensure robustness and efficiency.

## Features

- **CRUD Operations:** Users, Books, Authors, Orders
- **Database Support:** SQL Server, MongoDB
- **Caching:** Redis
- **Authentication & Authorization:** JWT Bearer
- **Password Security:** BCrypt
- **Exception Handling:** Middleware

## Technologies & Libraries

- **.NET** - The framework used for building the API.
- **EntityFrameworkCore** - ORM for data access and management.
- **Redis** - For caching and data replication to optimize performance.
- **JWT Bearer** - For handling authentication and authorization.
- **BCrypt** - For secure password hashing.
- **Middleware** - For exception handling and centralized error management.

## Architecture

### Responsibility Separation

The API follows the **Separation of Concerns** principle:
- **Controllers:** Manage HTTP requests and responses.
- **Services:** Encapsulate business logic.
- **Repositories:** Handle database interactions.

### Clean Architecture

The API adheres to **Clean Architecture** principles:
- **Core Layer:** Contains business entities and core logic.
- **Application Layer:** Manages application-specific logic and use cases.
- **Infrastructure Layer:** Manages data access and external integrations.
- **Presentation Layer:** Handles API endpoints and HTTP requests.

### Response Standard

The API uses a consistent **Response Standard**:
- **Success Responses:** Include status codes, data, and metadata.
- **Error Responses:** Provide error codes and messages for troubleshooting.

## Database Setup

### SQL Server

Used for relational data storage for Users, Books, Authors, and Orders.

### MongoDB

Provides flexible data storage for book-related information, supporting dynamic schema definitions.

## Caching with Redis

Redis is utilized for:
- **Data Caching:** Reduces database load and improves response times.
- **Replication:** Enhances data availability and durability.

## Authentication & Authorization

The API uses **JWT Bearer** tokens for:
- **Authentication:** Verifies user identity.
- **Authorization:** Controls access to resources based on user roles.

## Password Security

**BCrypt** is employed for:
- **Password Hashing:** Ensures secure storage of user passwords.

## Exception Handling

The API employs **Middleware** for:
- **Exception Handling:** Catches and manages exceptions centrally, providing consistent error responses.
