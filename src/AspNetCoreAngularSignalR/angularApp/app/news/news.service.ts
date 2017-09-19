import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { HubConnection } from '@aspnet/signalr-client';
import { NewsItem } from './models/news-item';
import { Store } from '@ngrx/store';
import { NewsState } from './store/news.state';
import * as NewsActions from './store/news.action';

@Injectable()
export class NewsService {

    private _hubConnection: HubConnection;

    constructor(private store: Store<any>) {
        this.init();
    }

    public send(newsItem: NewsItem): NewsItem {
        this._hubConnection.invoke('Send', newsItem);
        return newsItem;
    }

    public joinGroup(group: string): void {
        this._hubConnection.invoke('JoinGroup', group);
    }

    public leaveGroup(group: string): void {
        this._hubConnection.invoke('LeaveGroup', group);
    }

    private init() {

        this._hubConnection = new HubConnection('/looney');

        this._hubConnection.on('Send', (newsItem: NewsItem) => {
            this.store.dispatch(new NewsActions.ReceivedItemAction(newsItem));
        });

        this._hubConnection.on('JoinGroup', (data: string) => {
            this.store.dispatch(new NewsActions.ReceivedGroupJoinedAction(data));
        });

        this._hubConnection.on('LeaveGroup', (data: string) => {
            this.store.dispatch(new NewsActions.ReceivedGroupLeftAction(data));
        });

        this._hubConnection.start()
            .then(() => {
                console.log('Hub connection started')
            })
            .catch(err => {
                console.log('Error while establishing connection')
            });
    }

}
