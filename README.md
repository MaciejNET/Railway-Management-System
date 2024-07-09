# Railway-Management-System

## Description
This is a railway management system that allows users to book tickets, view their bookings, and cancel their bookings and get connections between two stations. The system also allows the admin to menage the trains, stations, and trips. The system is implmented using .NET 8, ASP.NET, Entity Framework Core, PostgreSQL.

To run the application you need to first typie in console:

```bash
docker-compose up
```

After docker-compose is up you can run application in your favourite IDE or using dotnet CLI:

```bash
cd RailwayManagementSystem.Api
dotnet run
```

Application is running on port 5000 fot http and 5001 for https.

In first run application should do migration and seed data to database.

Predefined admin account is:

```json
{
    "name": "Admin",
    "password": "Password123!"
}
```

Predefined passenger accounts is:

```json
{
    "email": "johndoe@mail.com",
    "password": "Password123!"
}
```

```json
{
    "email": "jack@mail.com",
    "password": "Password123!"
}
```

You can use swagger to test API. Swagger is available on https://localhost:5001/swagger/index.html
