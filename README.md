# N5 Assignment Permission Management API

    N5 company requests a Web API for registering user permissions, to carry out this task it is necessary to comply with the following steps:
        ● Create tables to manage employees , permission and permission types.
        ● Your system must allow that you have employess with "N" count of permissions type.
        ● Create a Web API using net core on Visual Studio and persist data on SQL Server.
        ● Make use of EntityFramework.

        ● The Web API must have 3 services “Request Permission”, “Modify Permission” and “Get Permissions”. Every service should persist a permission registry in an elasticsearch index, the register inserted in elasticsearch must contains the same structure of database table “permission”.
        ● Create apache kafka in local environment and create new topic where persist every operation a message with the next dto structure:
            Id: random Guid
            Name operation: “modify”, “request” or “get”.
            (desired) Making use of repository pattern and Unit of Work and CQRS pattern(Desired). Bear in mind that is required to stick to a proper service architecture so that creating different layers and dependency injection is a must-have.
        ● Add information logs in every api endpoint and log the name of operation using serilog as log library.
        ● Create Unit Testing and Integration Testing to call the three of the services.
        ● Use good practices as much as possible.0
        ● Prepare the solution to be containerized in a docker image.
        ● Prepare the solution to be deployed in kubernetes. (desired)
        ● Upload exercise to some repository (github, gitlab,etc).

### Key features: 
    Client-Server. Rest. Services and Respoitory layer + CQRS. Simple Unit od Work pattern. Async programming.

## Frameworks and Libraries
- [.NET 9](https://dotnet.microsoft.com/en-us/download/dotnet/9.0);
- [Entity Framework Core 9.02](https://github.com/dotnet/core/blob/main/release-notes/9.0/9.0.2/9.0.2.md);
- [Elasticsearch 8.5.1](https://www.elastic.co/downloads/past-releases/elasticsearch-8-5-1);
- [XUnit 2.9.3](https://xunit.net/releases/v2/2.9.3) (Unit Test and Integration Testing).

## Installation and Start

1. Install the certification manually. \
    [why? Microsoft is no longer creating directories when generates certificates] (https://learn.microsoft.com/en-us/dotnet/core/compatibility/aspnet-core/9.0/certificate-export) https://learn.microsoft.com/en-us/aspnet/core/security/docker-https?view=aspnetcore-9.0 \
    1.1 Go to solution folder in cmd/powershel \
    1.2 Run:  \
      ``` dotnet dev-certs https -ep aspnetapp.pfx -p felipe ``` \
    1.3 Copy the certificate and paste in  \
      ``` %USERPROFILE%\AppData\Roaming\ASP.NET\https ``` \
    1.4 docker should copies certificates to Alpine default cert folder (../home/app/.aspnet/https/) \
2. Run docker-compose\
  2.1 Go to solution folder in cmd/powershel\
  2.2 Run: \
    ``` docker-compose up --build ``` \
3. Migrations (Manual migration)\
  why? Not easy to run migration in dockerfile because yml do not consider the depends_on: command in a secuencial order. We can run migration following this steps or just in Visual Studio package manager console \
  3.1 Go to <Sln path>/DbMigration with cmd/powershel \
  3.2 Update N5Permission\DbMigrations\appsettings.json connection string by updateing local IP \
  2.3 Run:\
    ``` dotnet ef update ``` \

### Swagger
Navigate to ```[https://localhost:5001/swagger](https://localhost:8081/swagger/index.html)``` .

### Get Permissions by employee:

**HTTP GET**
https://localhost:8081/Permission?employeeId={ID}&api-version={Api_VERSION} \

Example:

curl -X 'GET' \
  'https://localhost:8081/Permission?employeeId=1&api-version={Api_VERSION}' \
  -H 'accept: */*' \

### Request Permission comparison result

  curl -X 'POST' \
    'https://localhost:8081/Permission?api-version=1' \
    -H 'accept: */*' \
    -H 'Content-Type: application/json' \
    -d '{
    "permission": string,
    "employeeId": int
  }'

Example
  curl -X 'POST' \
    'https://localhost:8081/Permission?api-version=1' \
    -H 'accept: */*' \
    -H 'Content-Type: application/json' \
    -d '{
    "permission": "Write",
    "employeeId": 1
  }'

### Modify Permission comparison result

curl -X 'POST' \
  'https://localhost:8081/Permission/ModifyPermission?api-version={Api_VERSION}' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '{
  Array<string>,
  "employeeId": int
}'

Example:

curl -X 'POST' \
  'https://localhost:8081/Permission/ModifyPermission?api-version=1' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '{
  "permissions": [
    "Read"
  ],
  "employeeId": 1
}'
### Status HTTP Response

Success: 200 HTTP

## Justifications
