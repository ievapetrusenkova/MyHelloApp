# Use the official .NET Core SDK image as the build environment
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env

# Set the working directory inside the container
WORKDIR /app

# Copy the C# project files to the container
COPY MyApp.csproj ./

# Restore dependencies
RUN dotnet restore

# Copy the entire project folder to the container
COPY . ./

# Build the C# application inside the container
RUN dotnet publish -c Release -o out

# Use the official .NET Core runtime image as the base image for the final container
FROM mcr.microsoft.com/dotnet/runtime:7.0

# Set the working directory inside the container
WORKDIR /app

# Copy the published files from the build-env container to the final container
COPY --from=build-env /app/out ./

# Expose the port on which the application is listening
EXPOSE 8081

# Specify the command to run the application when the container starts
CMD ["dotnet", "MyApp.dll"]
