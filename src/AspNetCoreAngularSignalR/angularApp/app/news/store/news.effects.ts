import 'rxjs/add/operator/map';
import 'rxjs/add/operator/switchMap';

import { Injectable } from '@angular/core';
import { Actions, Effect } from '@ngrx/effects';
import { Action } from '@ngrx/store';
import { of } from 'rxjs/observable/of';
import { Observable } from 'rxjs/Rx';

import * as newsAction from './news.action';
import { NewsItem } from '../models/news-item';
import { NewsService } from '../news.service';

@Injectable()
export class NewsEffects {

    @Effect() sendNewItem$: Observable<Action> = this.actions$.ofType(newsAction.SEND_NEWS_ITEM)
        .switchMap((action: newsAction.SendNewsItemAction) => {
            this.newsService.send(action.newsItem);
            return Observable.of(new newsAction.SendNewsItemActionComplete(action.newsItem));
        });

    constructor(
        private newsService: NewsService,
        private actions$: Actions
    ) { }
}
