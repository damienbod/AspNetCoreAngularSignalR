import { Action } from '@ngrx/store';
import { NewsItem } from '../models/news-item';

export const JOIN_GROUP = '[news] JOIN_GROUP';
export const LEAVE_GROUP = '[news] LEAVE_GROUP';
export const SEND_NEWS_ITEM = '[news] SEND_NEWS_ITEM';
export const RECIEVED_NEWS_ITEM = '[news] RECIEVED_NEWS_ITEM';
export const RECIEVED_GROUP_JOINED = '[news] RECIEVED_GROUP_JOINED';
export const RECIEVED_GROUP_LEFT = '[news] RECIEVED_GROUP_LEFT';

export class JoinGroupAction implements Action {
    readonly type = JOIN_GROUP;

    constructor(public group: string) { }
}

export class LeaveGroupAction implements Action {
    readonly type = LEAVE_GROUP;

    constructor(public group: string) { }
}

export class SendNewsItemAction implements Action {
    readonly type = SEND_NEWS_ITEM;

    constructor(public newsItem: NewsItem) { }
}

export class ReceivedItemAction implements Action {
    readonly type = RECIEVED_NEWS_ITEM;

    constructor(public newsItem: NewsItem) { }
}

export class ReceivedGroupJoinedAction implements Action {
    readonly type = RECIEVED_GROUP_JOINED;

    constructor(public group: string) { }
}

export class ReceivedGroupLeftAction implements Action {
    readonly type = RECIEVED_GROUP_LEFT;

    constructor(public group: string) { }
}

export type Actions
    = JoinGroupAction
    | LeaveGroupAction
    | SendNewsItemAction
    | ReceivedItemAction
    | ReceivedGroupJoinedAction
    | ReceivedGroupLeftAction;

