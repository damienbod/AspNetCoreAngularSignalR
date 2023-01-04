using Microsoft.AspNetCore.SignalR;

namespace AspNetCoreAngularSignalR.SignalRHubs;

public class LoopyHub : Hub
{
    public Task Send(string data)
    {
        return Clients.All.SendAsync("Send", data);
    }
}
