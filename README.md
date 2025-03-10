# ChatApp
A chat application built with ASP.NET Core for the backend, Angular for the frontend, and MySQL for the database. The project is organized by feature and follows SOLID principles for maintainability and scalability.

## Table of Contents

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
- **Logging:** ASP.NET Core Loggin
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
    "DefaultConnection": "Server=ChatApp.Database;Port=3306;Database=ChatApp;User=root;Password=password;"
  }
}
```

## Database Setup Using Docker
1. **Create a ==docker-compose.yml== file in the project root (or use the provided one):**
```yaml
version: '3.8'
services:
  mysql:
    image: mysql:8.0
    container_name: ChatApp.Database
    restart: always
    environment:
      MYSQL_ROOT_PASSWORD: password
      MYSQL_DATABASE: ChatApp
      MYSQL_USER: root
      MYSQL_PASSWORD: password
    ports:
      - "3306:3306"
    volumes:
      - mysql_data:/var/lib/mysql
  chatappapi:
    build:
      context: ./ChatApp.Api
      dockerfile: Dockerfile
    container_name: ChatApp.Api
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
      - ConnectionStrings__DefaultConnection=Server=ChatApp.Database;Port=3306;Database=ChatApp;User=root;Password=password;
    depends_on:
      - mysql
    ports:
      - "5000:80"
volumes:
  mysql_data:
```
2. **Run the containers:
```bash
docker-compose up --build
```
Your API will be available at ==https://localhost:7288== and will connect to the MySQL container.

3. **Apply EF Core migrations:**\
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
Common configurations (like CORS, logging, and authentication) are encapsulated in extension methods located in the ==/Extensions== folder.

- **Logging & Error Handling:**\
The API uses ASP.NET Core’s logging system along with structured logging and ProblemDetails for error responses.