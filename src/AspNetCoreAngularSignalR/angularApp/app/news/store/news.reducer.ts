import { NewsState } from './news.state';
import { NewsItem } from '../models/news-item';
import { Action } from '@ngrx/store';
import * as newsAction from './news.action';

export const initialState: NewsState = {
    newsItems: [],
    groups: ['group']
};

export function newsReducer(state = initialState, action: newsAction.Actions): NewsState {
    switch (action.type) {

        case newsAction.RECIEVED_GROUP_JOINED:
            return Object.assign({}, state, {
                newsItems: state.newsItems,
                groups: state.groups.concat(action.group)
            });

        case newsAction.RECIEVED_NEWS_ITEM:
            return Object.assign({}, state, {
                newsItems: state.newsItems.concat(action.newsItem),
                groups: state.groups
            });

        case newsAction.RECIEVED_GROUP_LEFT:
            return Object.assign({}, state, {
                newsItems: state.newsItems,
                groups: state.groups.filter(item => item !== action.group),
            });
        default:
            return state;

    }
}
