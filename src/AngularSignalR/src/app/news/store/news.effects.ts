import { Injectable } from '@angular/core';
import { Actions, ofType, createEffect } from '@ngrx/effects';
import { of } from 'rxjs';
import { catchError, map, switchMap } from 'rxjs/operators';

import * as newsAction from './news.action';
import { NewsService } from '../news.service';

@Injectable()
export class NewsEffects {
  constructor(private  newsService: NewsService, private actions$: Actions) {}

  sendNewsItemAction$ = createEffect(() =>
    this.actions$.pipe(
      ofType(newsAction.sendNewsItemAction),
      map((action) => action.payload),
      switchMap((payload) => {
        this.newsService.send(payload);
        return of(newsAction.sendNewsItemFinishedAction({payload}));
      })
    )
  );

  joinGroupAction$ = createEffect(() =>
    this.actions$.pipe(
      ofType(newsAction.joinGroupAction),
      map((action) => action.payload),
      switchMap((payload) => {
        this.newsService.joinGroup(payload);
        return of(newsAction.joinGroupFinishedAction({payload}));
      })
    )
  );
}




    @Effect()
    leaveGroup$: Observable<Action> = this.actions$.pipe(
        ofType<newsAction.LeaveGroupAction>(newsAction.LEAVE_GROUP),
        switchMap((action: newsAction.LeaveGroupAction) => {
            this.newsService.leaveGroup(action.group);
            return of(new newsAction.LeaveGroupActionComplete(action.group));
        })
    );

    @Effect()
    getAllGroups$: Observable<Action> = this.actions$.pipe(
        ofType(newsAction.SELECTALL_GROUPS),
        switchMap(() => {
            return this.newsService.getAllGroups().pipe(
                map((data: string[]) => {
                    return new newsAction.SelectAllGroupsActionComplete(data);
                }),
                catchError((error: any) => of(error)
                ));
        })
    );

