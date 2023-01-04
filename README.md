# SignalR with Angular and ASP.NET Core

[![.NET](https://github.com/damienbod/AspNetCoreAngularSignalR/actions/workflows/dotnet.yml/badge.svg)](https://github.com/damienbod/AspNetCoreAngularSignalR/actions/workflows/dotnet.yml)

- [Getting started with SignalR using ASP.NET Core and Angular](https://damienbod.com/2017/09/12/getting-started-with-signalr-using-asp-net-core-and-angular/)
- [SignalR Group messages with ngrx and Angular](https://damienbod.com/2017/09/18/signalr-group-messages-with-ngrx-and-angular/)
- [Using EF Core and SQLite to persist SignalR Group messages in ASP.NET Core](https://damienbod.com/2017/09/29/using-ef-core-and-sqlite-to-persist-signalr-group-messages-in-asp-net-core/)
- [Using Message Pack with ASP.NET Core SignalR](https://damienbod.com/2018/03/19/using-message-pack-with-asp-net-core-signalr/)
- [Uploading and sending image messages with ASP.NET Core SignalR](https://damienbod.com/2018/05/13/uploading-and-sending-image-messages-with-asp-net-core-signalr/)	
- [Securing an Angular SignalR client using JWT tokens with ASP.NET Core and IdentityServer4](https://damienbod.com/2017/10/16/securing-an-angular-signalr-client-using-jwt-tokens-with-asp-net-core-and-identityserver4/)
- [Implementing custom policies in ASP.NET Core using the HttpContext](https://damienbod.com/2017/10/23/implementing-custom-policies-in-asp-net-core-using-the-httpcontext/)
- [Sending Direct Messages using SignalR with ASP.NET core and Angular](https://damienbod.com/2017/12/05/sending-direct-messages-using-signalr-with-asp-net-core-and-angular/)
- [Implementing User Management with ASP.NET Core Identity and custom claims](https://damienbod.com/2018/10/30/implementing-user-management-with-asp-net-core-identity-and-custom-claims/)

## Run migrations

### Console

```
dotnet ef migrations add init_hub_db -c NewsContext
```

### Powershell

```
Add-Migration "init_hub_db" -c NewsContext  
```

## Running manually

```
Update-Database -Context NewsContext
```
## History

2023-01-04: Updated to .NET 7 and Angular 15

2022-01-28: Updated nuget packages

2021-11-12: Updated .NET 6, Angular

2021-03-14: Updated .NET 5, Angular

2021-02-28: Updated .NET 5, Angular CLI

2021-01-23: Updated .NET 5, updated ngrx implementation, latest CLI, prettier

2020-12-06: Updated .NET 5, Angular CLI

2020-11-29: Updated .NET 5, Angular 11.0.2

2020-03-22: Updated Angular 9.0.7

2020-02-25: Updated packages

2020-01-02: Updated packages

2019-11-17: Updated Angular 8.2.14

2019-09-24: Updated to ASP.NET Core 3.0, Angular 8.2.7

2019-09-20: Updated to ASP.NET Core 3.0 rc1

2019-09-14: Updated to ASP.NET Core 3.0 preview 9, Angular 8.2.6

2019-09-01: Updated to ASP.NET Core 3.0 preview 8, Angular 8.2.4

2019-08-13: Updated to ASP.NET Core 3.0 preview 8, Angular 8.2.2

2019-07-30: Updated to ASP.NET Core 3.0 preview 7, @microsoft/signalr

2019-07-30: Updated to Angular 8.1.3

2019-05-30: Updated to Angular 8.0.0

2019-04-30: Updated packages

2019-03-29: Updated to Angular 7.2.11

2019-03-15: Updated to Angular 7.2.9, Nuget packages

2019-02-24: Updated to Angular 7.2.6, Nuget packages

2019-02-11: Updated to Angular 7.2.4, NGRX, Nuget packages

2018-12-12: Updated to Angular 7.1.3

2018-12-04: Updated to .NET Core 2.2

2018-11-22: Updated  Angular 7.1.0

2018-10-26: Updated  Angular 7.0.1

2018-10-14: Updated  Angular 6.1.10, .NET Core 2.1.5, ASP.NET Core SignalR 1.0.4

2018-09-09: Updated  Angular 6.1.7

2018-08-03: Updated to .NET Core 2.1.2, Angular 6.1.0, bootstrap 4

2018-05-31: Updated to .NET Core 2.1

2018-05-27: Updated to Angular 6.0.3

2018-05-12: Updated to Angular 6.0.1, using message pack SignalR extension method in client

2018-05-08: Updated to .NET Core 2.1 rc1

2018-05-04: Updated to Angular 6

2018-04-22: Updating nuget packages and npm packages, @aspnet/signalr 1.0.0-preview2-final, Microsoft.AspNetCore.SignalR 1.0.0-preview2-final

2018-03-19: Added example for Message Pack

2018-03-14: Updated signalr Microsoft.AspNetCore.SignalR 1.0.0-preview1-final, Angular 5.2.8, @aspnet/signalr 1.0.0-preview1-update1

2017-10-15: Updated signalr 1.0.0-alpha2-final

## SignalR

https://github.com/aspnet/SignalR#readme

## npm feed

https://www.npmjs.com/package/msgpack5
