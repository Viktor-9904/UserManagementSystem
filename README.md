# User Management System

## Overview
This is an **ASP.NET Core MVC (.NET 8)** web application that fetches users from a public API, allows adding additional information, and saves them in a SQL Server database.  

### Features
- Fetch users from [JSONPlaceholder API](https://jsonplaceholder.typicode.com/users)
- Display users in a table with:
  - Editable fields: `Password`, `Note`, `IsActive`
- Save all users to a local SQL Server database (overwrites existing records)
- Display saved users from the database
- Simple validation for required fields
- Bootstrap-styled UI
- Success/error feedback with modals

---

## Requirements
- .NET 8 SDK
- SQL Server installed
- Visual Studio 2022

---

## Setup Instructions

1. **Clone the repository**
```bash
git clone https://github.com/YourUsername/UserManagementSystem.git
cd UserManagementSystem
```

2. **Check your database connection**
Default connection string in appsettings.json:
```bash
"ConnectionStrings": {
"DefaultConnection": "Server=.;Database=UserManagementDb;Trusted_Connection=True;"
}
```

3. **Run the application**

- Open the solution in Visual Studio
- Run the project (F5)

>On startup, the app automatically applies migrations and creates the database with Users and Addresses tables.

## Notes

>If you encounter database connection issues, check your SQL Server instance name and update the connection string.
>The app uses Dapper for database operations and Entity Framework Core only for migrations and initial database creation.
