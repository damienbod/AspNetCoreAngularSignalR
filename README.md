# SignalR with Angular

https://github.com/aspnet/SignalR#readme

## npm feed

https://www.npmjs.com/package/@aspnet/signalr-client

## MyGet feeds required for project

https://dotnet.myget.org/F/aspnetcore-ci-dev/api/v3/index.json

https://dotnet.myget.org/F/aspnetcore-ci-dev/npm/

## npm packages
https://dotnet.myget.org/feed/aspnetcore-ci-dev/package/npm/@aspnet/signalr-client
https://www.npmjs.com/package/msgpack5

    "msgpack5": "^3.5.1",
    "@aspnet/signalr-client": "1.0.0-alpha1-26973"


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