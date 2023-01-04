﻿using AspNetCoreAngularSignalR.Providers;
using Microsoft.AspNetCore.SignalR;

namespace AspNetCoreAngularSignalR.SignalRHubs;

public class NewsHub : Hub
{
    private NewsStore _newsStore;

    public NewsHub(NewsStore newsStore)
    {
        _newsStore = newsStore;
    }

    public Task Send(NewsItem newsItem)
    {
        if(!_newsStore.GroupExists(newsItem.NewsGroup))
        {
            throw new System.Exception("cannot send a news item to a group which does not exist.");
        }

        _newsStore.CreateNewItem(newsItem);
        return Clients.Group(newsItem.NewsGroup).SendAsync("Send", newsItem);
    }

    public async Task JoinGroup(string groupName)
    {
        if (!_newsStore.GroupExists(groupName))
        {
            throw new System.Exception("cannot join a group which does not exist.");
        }

        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        await Clients.Group(groupName).SendAsync("JoinGroup", groupName);

        var history = _newsStore.GetAllNewsItems(groupName);
        await Clients.Client(Context.ConnectionId).SendAsync("History", history);
    }

    public async Task LeaveGroup(string groupName)
    {
        if (!_newsStore.GroupExists(groupName))
        {
            throw new System.Exception("cannot leave a group which does not exist.");
        }

        await Clients.Group(groupName).SendAsync("LeaveGroup", groupName);
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
    }
}
