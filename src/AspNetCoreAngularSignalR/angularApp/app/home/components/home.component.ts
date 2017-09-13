import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs/Observable';
import { HubConnection, HttpConnection } from '@aspnet/signalr-client';

@Component({
    selector: 'app-home-component',
    templateUrl: './home.component.html'
})

export class HomeComponent implements OnInit {
    private _hubConnection: HubConnection;
    public async: any;
    message = '';
    messages: string[] = [];

    constructor(private store: Store<any>) {
    }

    public sendMessage(): void {
        const data = `Sent: ${this.message}`;

        this._hubConnection.invoke('Send', data);
        this.messages.push(data);
    }

    ngOnInit() {
        const httpConnection = new HttpConnection('/loopy');
        this._hubConnection = new HubConnection(httpConnection);

        this._hubConnection.on('Send', (data: any) => {
            const recieved = `Recieved: ${data}`;
            this.messages.push(recieved);
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
