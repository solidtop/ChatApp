# ChatApp
A chat application built with ASP.NET Core for the backend, Angular for the frontend, and MySQL for the database. The project is organized by feature and follows SOLID principles for maintainability and scalability.

## Table of Contents

- [Features]
- [Tech Stack]
- [Prerequisites]
- [Setup and Installation]
	- [Configuration] 
	- [Database Setup using Docker]
- [Running the Application]
- [Development Workflow]
- [API Endpoints]

## Features
- **Authentication:**\
	Register, login and logout endpoints using ASP.NET Core Identity
- **Account Management:**\
	Endpoints for managing the current user's private account details.
- **User Management:**\
	Separate endpoints for public user data with different levels of detail (e.g., summary and detailed views).
- **Role-Based Access Control:**\
	Default roles (Admin, Moderator, User) seeded in development.
- **Standardized Error Responses:**\
	Uses ProblemDetails (RFC 7807) for consistent error handling

## Tech Stack
- **Backend:** ASP.NET Core, C#
- **Frontend:** Angular
- **Database:** MySQL
- **Containerization:** Docker, Docker Compose
- **Logging:** ASP.NET Core Logging
- **Dependency Injection & Modular Architecture:** SOLID principles with feature-based organization

## Prerequisites
- .NET 9 SDK or later
- Node.js and npm
- Docker Desktop
- Angular CLI

## Setup and Installation
### Configuration
1. **Clone the repository:**
```bash
git clone https://github.com/solidtop/ChatApp.git
cd ChatApp
```
2. **Set up secrets for development:**\
For development, use the ASP.NET Core Secret Manager to store admin credentials securely:
```bash
cd ChatApp.Server
dotnet user-secrets init
dotnet user-secrets set "AdminCredentials:Username" "username"
dotnet user-secrets set "AdminCredentials:Email" "admin@admin.com"
dotnet user-secrets set "AdminCredentials:Password" "password"
```
3. **Configure connection strings:**\
In your ==appsettings.Development.json==, set your connection string. Note that when running in Docker, the MySQL host should reference the container’s name:
```json
{
  "ConnectionStrings": {
    "Database": "Server=chatapp.database; Port=3306; Database=chatapp; User=root; Password=password;"
  }
}
```

### Database Setup Using Docker 
If using Visual Studio, you can run the 'Docker Compose' command to automatically build the required containers.

**Run the containers using CLI:**
```bash
docker-compose up --build
```
Your Server will be available at ==https://localhost:5001== and will connect to the MySQL container.

**Apply EF Core migrations:**\
Navigate to the Server project folder and run:
```bash
dotnet ef database update
```

## Running the Application
- **Backend (Server):**\
Run the Server from Visual Studio or using ==dotnet run== in the ChatApp.Server folder.
- **Frontend (Angular)**\
Navigate to the ==chatapp.client== folder, install dependencies and run:
```bash
npm install
ng serve
```

## Development Workflow
- **Code Organization:**\
The project is organized by feature. For example:

- ChatApp.Server/Features/Auth for authentication endpoints.
- ChatApp.Server/Features/Account for account management.
- ChatApp.Server/Features/Users for public user data.

- **Extension Methods:**\
Common configurations (like CORS, and authentication) are encapsulated in extension methods located in the ==/Extensions== folder.

- **Logging & Error Handling:**\
The API uses ASP.NET Core’s logging system along with structured logging and ProblemDetails for error responses.

## API Endpoints
- **Authentication:**
	- ==POST== /api/auth/register
	- ==POST== /api/auth/login
	- ==POST== /api/auth/logout

- **Account:**
	- ==GET== /api/account/details 
	- ==PUT== /api/account/display-color 
	- ==PUT== /api/account/avatar 

- **Users:**
	- ==GET== /api/users
	- ==GET== /api/users/{id}

- **Avatars:**
	- ==GET== /api/avatars
	- ==GET== /api/avatars/{id}