# Cinema Ticket Purchasing System

A server-rendered web app for browsing cinema screenings and booking seats, built with ASP.NET Core MVC, Entity Framework Core and PostgreSQL.

Lab project for the EGUI course at Warsaw University of Technology. This is the earlier MVC version; the later version with a React client is [MVC-React-Cinema-Ticket-Purchasing-System](https://github.com/Tpchck/MVC-React-Cinema-Ticket-Purchasing-System).

## Build and run

Prerequisites: .NET 10 SDK, PostgreSQL, the `dotnet-ef` tool.

Set `ConnectionStrings:DefaultConnection` in `appsettings.json` to your database, then:

```bash
dotnet ef database update
dotnet run
```

The app runs at http://localhost:5238.
