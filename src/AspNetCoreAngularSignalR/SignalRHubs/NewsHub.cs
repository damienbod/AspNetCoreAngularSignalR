using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace AspNetCoreAngularSignalR.SignalRHubs
{
    public class NewsHub : Hub
    {
        public Task Send(NewsItem newsItem)
        {
            return Clients.Group(newsItem.NewsGroup).InvokeAsync("Send", newsItem);
        }

        public async Task JoinGroup(string groupName)
        {
            await Groups.AddAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).InvokeAsync("JoinGroup", groupName);
        }

        public async Task LeaveGroup(string groupName)
        {
            await Clients.Group(groupName).InvokeAsync("LeaveGroup", groupName);
            await Groups.RemoveAsync(Context.ConnectionId, groupName);
        }
    }
}
