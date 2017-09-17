import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { HubConnection } from '@aspnet/signalr-client';
import { NewsItem } from '../models/news-item';

@Component({
    selector: 'app-news-component',
    templateUrl: './news.component.html'
})

export class NewsComponent implements OnInit {
    private _hubConnection: HubConnection;
    public async: any;
    newsItem: NewsItem;
    newsItems: NewsItem[];

    constructor() {
        this.newsItem = new NewsItem();
        this.newsItem.AddData('header', 'text', 'author', 'myGroup');

        this.newsItems = new Array<NewsItem>();
    }

    public sendNewsItem(): void {
        this._hubConnection.invoke('Send', this.newsItem);
        console.log('sendNewsItem.Send')
        console.log(this.newsItem)
        //this.newsItems.push(this.newsItem);
    }

    public join(): void {
        this._hubConnection.invoke('JoinGroup', 'myGroup');
    }

    ngOnInit() {

        this._hubConnection = new HubConnection('/looney');

        this._hubConnection.on('Send', (data: NewsItem) => {
            console.log('_hubConnection.on.Send')
            console.log(data)
            this.newsItems.push(data);
        });

        this._hubConnection.on('JoinGroup', (data: string) => {
            console.log('joined: ' + data)
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
