#!/bin/bash

# Install PostgreSQL client tools and dotnet-ef
apt-get update && apt-get install -y postgresql-client
dotnet tool install --global dotnet-ef

# Add .NET tools to PATH for the current session
export PATH="$PATH:/root/.dotnet/tools"

# Wait until PostgreSQL is ready
until pg_isready -h task-db -p 5432; do
  echo "Waiting for PostgreSQL..."
  sleep 2
done

# Generate a new migration if there are pending model changes
TIMESTAMP=$(date +"%Y%m%d%H%M%S")
MIGRATION_NAME="AutoMigration_$TIMESTAMP"

echo "Checking for pending migrations..."
dotnet ef migrations add $MIGRATION_NAME --project TaskBlaster.TaskManagement.API --startup-project TaskBlaster.TaskManagement.API || echo "No new migrations needed"

# Apply migrations to the database
dotnet ef database update
