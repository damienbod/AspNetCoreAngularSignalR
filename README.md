# SignalR with Angular and ASP.NET Core

[![Build status](https://ci.appveyor.com/api/projects/status/9si04k3e5x2ksrj4?svg=true)](https://ci.appveyor.com/project/damienbod/aspnetcoreangularsignalr)


## blogs:

<ul>	
	
<li><a href="https://damienbod.com/2017/09/12/getting-started-with-signalr-using-asp-net-core-and-angular/">Getting started with SignalR using ASP.NET Core and Angular</a></li>
<li><a href="https://damienbod.com/2017/09/18/signalr-group-messages-with-ngrx-and-angular/">SignalR Group messages with ngrx and Angular</a></li>
<li><a href="https://damienbod.com/2017/09/29/using-ef-core-and-sqlite-to-persist-signalr-group-messages-in-asp-net-core/">Using EF Core and SQLite to persist SignalR Group messages in ASP.NET Core</a></li>
<li><a href="https://damienbod.com/2017/10/16/securing-an-angular-signalr-client-using-jwt-tokens-with-asp-net-core-and-identityserver4/">Securing an Angular SignalR client using JWT tokens with ASP.NET Core and IdentityServer4</a></li>
<li><a href="https://damienbod.com/2017/10/23/implementing-custom-policies-in-asp-net-core-using-the-httpcontext/">Implementing custom policies in ASP.NET Core using the HttpContext</a></li>
<li><a href="https://damienbod.com/2017/12/05/sending-direct-messages-using-signalr-with-asp-net-core-and-angular/">Sending Direct Messages using SignalR with ASP.NET core and Angular</a> </li>
<li><a href="https://damienbod.com/2018/03/19/using-message-pack-with-asp-net-core-signalr/">Using Message Pack with ASP.NET Core SignalR</a> </li>
</ul>

## History

2018-05-04: Updating nuget Angular 6

2018-04-22: Updating nuget packages and npm packages

2018-03-19: Added example for Message Pack

2018-03-14: Updated signalr Microsoft.AspNetCore.SignalR 1.0.0-preview1-final, Angular 5.2.8, @aspnet/signalr 1.0.0-preview1-update1

2017-10-15: Updated signalr 1.0.0-alpha2-final

## SignalR

https://github.com/aspnet/SignalR#readme

## npm feed

https://www.npmjs.com/package/@aspnet/signalr
https://www.npmjs.com/package/msgpack5

## MyGet feeds required for project if using the latest dev

https://dotnet.myget.org/F/aspnetcore-ci-dev/api/v3/index.json

https://dotnet.myget.org/F/aspnetcore-ci-dev/npm/
https://dotnet.myget.org/feed/aspnetcore-ci-dev/package/npm/@aspnet/signalr-client

## npm packages

	"@aspnet/signalr": "^1.0.0-preview1-update1",
	"msgpack5": "4.0.2",


# Production Build

The new uglifyjs-webpack-plugin is required.

## Step 1: 
in the package.json add:

"uglifyjs-webpack-plugin": "1.0.0-beta.2",

## Step 2: 

add the UglifyJSPlugin webpack plugin (S is capital), in the webpack production build file

const UglifyJSPlugin = require('uglifyjs-webpack-plugin');

## Step 3: 

Update the webpack production build

```
        new UglifyJSPlugin({
            parallel: {
                cache: true,
                workers: 2
            }
        }),
```
