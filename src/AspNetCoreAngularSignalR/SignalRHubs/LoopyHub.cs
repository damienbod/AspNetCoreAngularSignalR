using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace AspNetCoreSignalr.SignalRHubs
{
    public class LoopyHub : Hub
    {
        public Task Send(string data)
        {
            return Clients.All.InvokeAsync("Send", data);
        }
    }
}
