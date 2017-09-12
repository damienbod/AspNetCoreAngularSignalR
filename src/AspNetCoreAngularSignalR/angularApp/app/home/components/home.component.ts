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

    constructor(private store: Store<any>) {
    }

    public TriggerCallback(): void {
        const data = 'testmessage';
        this._hubConnection.invoke('Send', data);
    }

    ngOnInit() {
        const httpConnection = new HttpConnection('http://localhost:5000/loopy');
        this._hubConnection = new HubConnection(httpConnection);

        this._hubConnection.on('Send', data => {
            console.log('Got notification: ' + data);
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
