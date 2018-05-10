import { Component, OnInit } from '@angular/core';
import { HubConnection } from '@aspnet/signalr';
import * as signalR from '@aspnet/signalr';

import { ImageMessage } from '../imagemessage';

@Component({
    selector: 'app-images-component',
    templateUrl: './images.component.html'
})

export class ImagesComponent implements OnInit {
    private _hubConnection: HubConnection | undefined;
    public async: any;
    message = '';
    messages: string[] = [];

    images: ImageMessage[] = [];

    constructor() {
    }

    public sendMessage(): void {
        const data = `Sent: ${this.message}`;

        if (this._hubConnection) {
            this._hubConnection.invoke('SendFileNameUpload', data);
        }
        this.messages.push(data);
    }

    ngOnInit() {
        this._hubConnection = new signalR.HubConnectionBuilder()
            .withUrl('https://localhost:44324/zub')
            .configureLogging(signalR.LogLevel.Trace)
            .build();

        this._hubConnection.stop();

        this._hubConnection.start().catch(err => {
            console.error(err.toString())
        });  

        this._hubConnection.on('SendFileNameUpload', (data: any) => {
            const received = `Received: ${data}`;
            this.messages.push(received);
        });

        this._hubConnection.on('ImageMessage', (data: any) => {
            this.images.push(data);
        });

    }

}
