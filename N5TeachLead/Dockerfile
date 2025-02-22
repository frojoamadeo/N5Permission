# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081
#ENV DB_CONNECTION_STRING="Server=192.168.1.109,1433;Initial Catalog=local_n5_permission_db;User Id=sa;password=1234;TrustServerCertificate=True;ApplicationIntent=ReadWrite;Application Name=Local_Permission_Server;"
#ENV ELASTICSEARCH_URI = "http://192.168.1.109:9200"
#ENV PRIVATE_IP $(hostname -I)  
ENV ASPNETCORE_Kestrel__Certificates__Default__Enabled=false

#ENV ASPNETCORE_ENVIRONMENT "test"
USER root
RUN apk add --no-cache icu-libs sudo
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

USER $APP_UID

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["N5TeachLead/N5TeachLeadPermissions.csproj", "N5TeachLead/"]
COPY ["AppServices/AppServices.csproj", "AppServices/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Persistance/Persistance.csproj", "Persistance/"]
COPY ["Services/Infraestructure.csproj", "Services/"]
COPY ["Repository/Repository.csproj", "Repository/"]
COPY ["DbMigrations/DbMigrations.csproj", "DbMigrations/"]

COPY ["AppServices.Tests/AppServices.Tests.csproj", "Tests/"]
COPY ["Infraestructure.Tests/Infraestructure.Tests.csproj", "Tests/"]
COPY ["Repository.Tests/Repository.Tests.csproj", "Tests/"]


RUN dotnet restore "./N5TeachLead/N5TeachLeadPermissions.csproj"
COPY . .

RUN     apk add doas; \
        adduser felipe; \
        echo '123' | chpasswd; \
        echo 'permit felipe as root' > /etc/doas.d/doas.conf

WORKDIR "/src/N5TeachLead"
RUN dotnet build "./N5TeachLeadPermissions.csproj" -c $BUILD_CONFIGURATION -o /app/build

WORKDIR "/src/Tests"

# run tests on docker build
RUN dotnet test ./AppServices.Tests.csproj
RUN dotnet test ./Infraestructure.Tests.csproj
RUN dotnet test ./Repository.Tests.csproj

WORKDIR "/src/N5TeachLead"

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release

#RUN dotnet dev-certs https -ep aspnet.pfx -p felipe
#COPY --chmod=0755 --from=build https\* /https/

#RUN dotnet dev-certs https -ep %USERPROFILE%\AppData\Roaming\ASP.NET\https\aspnetapp.pfx -p felipe
RUN dotnet dev-certs https --clean
#RUN dotnet dev-certs https --trust

RUN dotnet publish "./N5TeachLeadPermissions.csproj" -c $BUILD_CONFIGURATION --self-contained -o /app/publish /p:UseAppHost=true



ENV PATH $PATH:/root/.dotnet/tools
RUN dotnet tool install -g dotnet-ef  --version 9.0.2

#WORKDIR "/src/DbMigrations"

#USER root
ENV DB_CONNECTION_STRING="Server=sqlserver,1439;Initial Catalog=local_n5_permission_db;User Id=sa;password=Password12345!;TrustServerCertificate=True;ApplicationIntent=ReadWrite;Application Name=Local_Permission_Server;"

RUN dotnet tool install --global dotnet-ef --version 9.0.2
ENV PATH="$PATH:/root/.dotnet/tools"
#RUN dotnet ef migrations add InitialCreate
#RUN /root/.dotnet/tools/dotnet-ef database update

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app



COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "N5TeachLeadPermissions.dll"]