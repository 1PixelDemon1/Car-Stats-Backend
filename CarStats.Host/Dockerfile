# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5226


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CarStats.Host/CarStats.Host.csproj", "CarStats.Host/"]
COPY ["Modules/AuthModule/CarStats.Auth.Presentation/CarStats.Auth.Presentation.csproj", "Modules/AuthModule/CarStats.Auth.Presentation/"]
COPY ["SharedKernel/CarStats.Abstractions/CarStats.Abstractions.csproj", "SharedKernel/CarStats.Abstractions/"]
COPY ["Modules/CarModule/CarStats.Car.Presentation/CarStats.Car.Presentation.csproj", "Modules/CarModule/CarStats.Car.Presentation/"]
COPY ["SharedKernel/CarStats.Utils/CarStats.Utils.csproj", "SharedKernel/CarStats.Utils/"]
COPY ["Modules/UserModule/CarStats.User.Presentation/CarStats.User.Presentation.csproj", "Modules/UserModule/CarStats.User.Presentation/"]
RUN dotnet restore "./CarStats.Host/CarStats.Host.csproj"
COPY . .
WORKDIR "/src/CarStats.Host"
RUN dotnet build "./CarStats.Host.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CarStats.Host.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarStats.Host.dll"]