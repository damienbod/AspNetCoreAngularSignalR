using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreAngularSignalR.SignalRHubs
{
    public class NewsHub : Hub
    {
        private List<string> groups = new List<string>();

        public Task Send(NewsItem newsItem)
        {
            return Clients.Group(newsItem.NewsGroup).InvokeAsync("Send", newsItem);
        }

        public async Task JoinGroup(string groupName)
        {
            await Groups.AddAsync(Context.ConnectionId, groupName);
            // $"{Context.ConnectionId} joined {groupName}"
            await Clients.Group(groupName).InvokeAsync("JoinGroup", groupName);
        }

        public async Task LeaveGroup(string groupName)
        {
            await Groups.RemoveAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).InvokeAsync("LeaveGroup", groupName);
        }
    }
}
