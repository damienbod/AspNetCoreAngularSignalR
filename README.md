# SignalR with Angular and ASP.NET Core

[![Build status](https://ci.appveyor.com/api/projects/status/9si04k3e5x2ksrj4?svg=true)](https://ci.appveyor.com/project/damienbod/aspnetcoreangularsignalr)


## blogs:

<ul>	
	
<li><a href="https://damienbod.com/2017/09/12/getting-started-with-signalr-using-asp-net-core-and-angular/">Getting started with SignalR using ASP.NET Core and Angular</a></li>
<li><a href="https://damienbod.com/2017/09/18/signalr-group-messages-with-ngrx-and-angular/">SignalR Group messages with ngrx and Angular</a></li>
<li><a href="https://damienbod.com/2017/09/29/using-ef-core-and-sqlite-to-persist-signalr-group-messages-in-asp-net-core/">Using EF Core and SQLite to persist SignalR Group messages in ASP.NET Core</a></li>

</ul>

## SignalR

https://github.com/aspnet/SignalR#readme

## npm feed

https://www.npmjs.com/package/@aspnet/signalr-client
https://www.npmjs.com/package/msgpack5

## MyGet feeds required for project

https://dotnet.myget.org/F/aspnetcore-ci-dev/api/v3/index.json

https://dotnet.myget.org/F/aspnetcore-ci-dev/npm/
https://dotnet.myget.org/feed/aspnetcore-ci-dev/package/npm/@aspnet/signalr-client

## npm packages

    "msgpack5": "^3.5.1",
    "@aspnet/signalr-client": "1.0.0-alpha1-final"


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
