# NetAppSqlServer - SQL Server Query Tool

NetAppSqlServer is a simple command-line interface (CLI) tool written in C# and .NET 9 that allows users to execute SQL queries against a SQL Server database interactively. It provides formatted table output for query results, making it easy to inspect data directly from the terminal.

## Features

- **Interactive Querying**: Prompt-based interface to enter and execute SQL queries.
- **Formatted Table Output**: Automatically adjusts column widths to display query results in a clean, readable grid.
- **Configuration-driven**: Connection strings are managed via `appsettings.json`.
- **Cross-platform**: Built on .NET 9, it can be run on Windows, macOS, and Linux.

## Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- Access to a SQL Server instance.

## Getting Started

### 1. Configuration

Before running the application, update the `appsettings.json` file in the root directory with your SQL Server connection string.

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=YOUR_DATABASE;User Id=YOUR_USER;Password=YOUR_PASSWORD;TrustServerCertificate=True;"
  }
}
```

### 2. Building the Project

You can build the project using the provided `Makefile` or the .NET CLI.

**Using Make:**
```bash
make build
```

**Using .NET CLI:**
```bash
dotnet build
```

### 3. Running the Application

**Using Make:**
```bash
make run
```

**Using .NET CLI:**
```bash
dotnet run
```

## Usage Examples

Once the application is running, you will be prompted to enter your SQL queries.

### Selecting Data
```text
SQL Server Query Tool
---------------------

Enter your SQL query (or type 'exit' to quit):
SELECT TOP 5 Name, ProductNumber FROM Production.Product

+--------------------------+----------------+
| Name                     | ProductNumber  |
+--------------------------+----------------+
| Adjustable Race          | AR-5381        |
| Bearing Ball             | BA-8327        |
| BB Ball Bearing          | BE-2349        |
| Headset Ball Bearings    | BE-2908        |
| Blade                    | BL-2036        |
+--------------------------+----------------+
```

### Executing an Update/Insert
```text
Enter your SQL query (or type 'exit' to quit):
UPDATE MyTable SET Status = 'Active' WHERE Id = 1

Query executed successfully, but returned no results or it was not a SELECT statement.
1 row(s) affected.
```

### Exiting
Type `exit` or press `Ctrl+C` to quit the application.

```text
Enter your SQL query (or type 'exit' to quit):
exit
```

## Makefile Commands

- `make build`: Restores dependencies and builds the project.
- `make run`: Builds and runs the application.
- `make clean`: Removes build artifacts.
- `make publish`: Publishes the application for release.
