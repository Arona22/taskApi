# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy only the .csproj files and restore dependencies
COPY TaskBlaster.TaskManagement/TaskBlaster.TaskManagement.API/*.csproj TaskBlaster.TaskManagement/TaskBlaster.TaskManagement.API/
COPY TaskBlaster.TaskManagement/TaskBlaster.TaskManagement.DAL/*.csproj TaskBlaster.TaskManagement/TaskBlaster.TaskManagement.DAL/
COPY TaskBlaster.TaskManagement/TaskBlaster.TaskManagement.Models/*.csproj TaskBlaster.TaskManagement/TaskBlaster.TaskManagement.Models/
RUN dotnet restore TaskBlaster.TaskManagement/TaskBlaster.TaskManagement.API/*.csproj

# Copy the entire source code
COPY . .

# Set the working directory to the Tasks API project
WORKDIR /app/TaskBlaster.TaskManagement/TaskBlaster.TaskManagement.API

# Build and publish the project
RUN dotnet publish -c Release -o out

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copy published output from the build environment
COPY --from=build-env /app/TaskBlaster.TaskManagement/TaskBlaster.TaskManagement.API/out .

# Expose the port the API will run on
EXPOSE 8080

# Set the entry point for the container to start the application
ENTRYPOINT ["dotnet", "TaskBlaster.TaskManagement.API.dll"]
