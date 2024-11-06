# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy only the solution file and API project files, restore dependencies
COPY TaskBlaster.TaskManagement.API/*.csproj TaskBlaster.TaskManagement.API/
COPY TaskBlaster.TaskManagement.DAL/*.csproj TaskBlaster.TaskManagement.DAL/
COPY TaskBlaster.TaskManagement.Models/*.csproj TaskBlaster.TaskManagement.Models/
RUN dotnet restore TaskBlaster.TaskManagement.API/*.csproj

# Copy the full source code to the container
COPY . .

# Build the API project
WORKDIR /app/TaskBlaster.TaskManagement.API
RUN dotnet publish -c Release -o out

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/TaskBlaster.TaskManagement.API/out .

# Expose the port the API will run on
EXPOSE 80

# Set the entry point for the container
ENTRYPOINT ["dotnet", "TaskBlaster.TaskManagement.API.dll"]
