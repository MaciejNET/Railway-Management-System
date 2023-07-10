# Railway-Management-System

## Description
This is a railway management system that allows users to book tickets, view their bookings, and cancel their bookings and get connections between two stations. The system also allows the admin to menage the trains, stations, and trips. The system is implmented using .NET 6, ASP.NET, Entity Framework Core, PostgreSQL.

App is running on PostgreSQL database, so you need to install it first. You can download it from [here](https://www.postgresql.org/download/). PostgreSQL is running on port 5433, with user Postgres and empty password. If you want to change it, you can do it in RailwayManagementSystem.API/appsettings.json file.

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
