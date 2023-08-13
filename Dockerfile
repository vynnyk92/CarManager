# BUILD STAGE
FROM mcr.microsoft.com/dotnet/sdk:6.0 as build
WORKDIR /source
EXPOSE 80
EXPOSE 443

COPY ./CarManager.sln ./CarManager.sln
COPY ./src/CarManager/CarManager.csproj ./src/CarManager/CarManager.csproj
COPY ./test/CarManager.IntegrationTests/CarManager.IntegrationTests.csproj ./test/CarManager.IntegrationTests/CarManager.IntegrationTests.csproj
COPY ./test/CarManager.UnitTests/CarManager.UnitTests.csproj ./test/CarManager.UnitTests/CarManager.UnitTests.csproj

RUN dotnet restore

# Copy the rest of the code into the container
COPY . .

RUN dotnet build src/CarManager/CarManager.csproj --configuration Release --output /app

# RUN ["pwsh", "./scripts/test.ps1"]

# RUN dotnet test test/CarManager.UnitTests/CarManager.UnitTests.csproj

## Publish the application
FROM build AS publish
WORKDIR /source/src/CarManager/
RUN dotnet publish --configuration Release --output /app --no-restore

## Build the image for running in EKS
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 5144
EXPOSE 443

COPY --from=publish /app .

ENTRYPOINT ["dotnet", "CarManager.dll"]
