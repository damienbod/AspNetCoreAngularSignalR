using AspNetCoreAngularSignalR.Controllers;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace AspNetCoreAngularSignalR.SignalRHubs
{
    public class NewsHub : Hub
    {
        private readonly NewsProvider _newsProvider;

        public NewsHub(NewsProvider newsProvider)
        {
            _newsProvider = newsProvider;
        }

        private List<string> groups = new List<string>();

        public Task Send(string groupName, NewsItem newsItem)
        {
            if(!_newsProvider.GroupExists(groupName))
            {
                throw new Exception("Group does not exist");
            }

            return Clients.Group(groupName).InvokeAsync("Send", newsItem);
        }

        public async Task CreateGroup(string groupName)
        {
            if(!_newsProvider.GroupExists(groupName))
            {
                throw new Exception("Group does not exist");
            }

            await Groups.AddAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).InvokeAsync("Send", $"{Context.ConnectionId} joined {groupName}");
        }

        public async Task JoinGroup(string groupName)
        {
            if(!_newsProvider.GroupExists(groupName))
            {
                throw new Exception("Group does not exist");
            }

            await Groups.AddAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).InvokeAsync("Send", $"{Context.ConnectionId} joined {groupName}");

            var news = _newsProvider.GetAllNewItems(groupName).OrderByDescending(o => o.Created);
            foreach (var item in news)
            {
                await Clients.Client(Context.ConnectionId).InvokeAsync("Send", item);
            }
        }

        public async Task LeaveGroup(string groupName)
        {
            if (!_newsProvider.GroupExists(groupName))
            {
                throw new Exception("Group does not exist");
            }

            await Groups.RemoveAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).InvokeAsync("Send", $"{Context.ConnectionId} left {groupName}");
        }
    }
}
