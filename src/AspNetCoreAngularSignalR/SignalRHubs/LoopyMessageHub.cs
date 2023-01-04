﻿using Dtos;
using Microsoft.AspNetCore.SignalR;

namespace AspNetCoreAngularSignalR.SignalRHubs;

// Send messages using Message Pack binary formatter
public class LoopyMessageHub : Hub
{
    public Task Send(MessageDto data)
    {
        return Clients.All.SendAsync("Send", data);
    }
}
