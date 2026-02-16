# Makefile for NetAppSqlServer

# Default target
all: build

# Restore dependencies
restore:
	dotnet restore

# Build the project
build: restore
	dotnet build --no-restore

# Run the project
run: build
	dotnet run --no-build

# Clean build artifacts
clean:
	dotnet clean

# Publish the project
publish:
	dotnet publish -c Release
