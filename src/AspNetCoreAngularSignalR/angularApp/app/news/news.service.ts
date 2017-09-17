import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { HubConnection } from '@aspnet/signalr-client';
import { NewsItem } from './models/news-item';

@Injectable()
export class NewsService {

    private _hubConnection: HubConnection;

    constructor() {
        this.init();
    }

    public send(newsItem: NewsItem): void {
        this._hubConnection.invoke('Send', newsItem);
        // TODO send event
        //this.newsItems.push(this.newsItem);
    }

    public joinGroup(group: string): void {
        this._hubConnection.invoke('JoinGroup', group);
    }

    public leaveGroup(group: string): void {
        this._hubConnection.invoke('LeaveGroup', group);
    }

    init() {

        this._hubConnection = new HubConnection('/looney');

        this._hubConnection.on('Send', (data: NewsItem) => {
            console.log('_hubConnection.on.Send')
            console.log(data)
            // TODO send event this.newsItems.push(data);
        });

        this._hubConnection.on('JoinGroup', (data: string) => {
            // TODO send event
            console.log('joined: ' + data)
        });

        this._hubConnection.on('LeaveGroup', (data: string) => {
            // TODO send event
            console.log('left: ' + data)
        });

        this._hubConnection.start()
            .then(() => {
                // TODO send event
                console.log('Hub connection started')
            })
            .catch(err => {
                console.log('Error while establishing connection')
            });
    }

}
