# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["N5TeachLead/N5TeachLeadPermissions.csproj", "N5TeachLead/"]
COPY ["AppServices/AppServices.csproj", "AppServices/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Persistance/Persistance.csproj", "Persistance/"]
COPY ["Services/Infraestructure.csproj", "Services/"]
COPY ["Repository/Repository.csproj", "Repository/"]
RUN dotnet restore "./N5TeachLead/N5TeachLeadPermissions.csproj"
RUN dotnet restore "./Domain/Domain.csproj"
RUN dotnet restore "./Persistance/Persistance.csproj"
RUN dotnet restore "./Services/Infraestructure.csproj"
RUN dotnet restore "./Repository/Repository.csproj"
COPY . .
WORKDIR "/src/N5TeachLead"
RUN dotnet build "./N5TeachLeadPermissions.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./N5TeachLeadPermissions.csproj" -c $BUILD_CONFIGURATION --self-contained -o /app/publish /p:UseAppHost=true

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["ls", "."]