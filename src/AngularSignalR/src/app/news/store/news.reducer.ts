import { NewsState } from './news.state';
import * as newsAction from './news.action';
import { createReducer, on, Action } from '@ngrx/store';

export const initialState: NewsState = {
  news: {
      newsItems: [],
      groups: ['IT', 'global', 'sport']
  }
};


const newsReducerInternal = createReducer(
  initialState,
  on(
    newsAction.joinGroupAction,
    newsAction.joinGroupFinishedAction,
    newsAction.leaveGroupAction,
    newsAction.leaveGroupFinishedAction,
    newsAction.recieveGroupJoinedAction,
    newsAction.recieveGroupLeftAction,
    newsAction.recieveNewsGroupHistoryAction,
    newsAction.recieveNewsItemAction,
    newsAction.selectAllNewsGroupsAction,
    newsAction.selectAllNewsGroupsFinishedAction,
    newsAction.sendNewsItemAction,
    newsAction.sendNewsItemFinishedAction,
    (state) => ({
      ...state
    })
  ),
  on(newsAction.recieveGroupJoinedAction, (state, { payload }) => ({
    ...state,
    things: [...state.things, payload],
  })),
);

export function newsReducer(
  state: NewsState | undefined,
  action: Action
): any {
  return newsReducerInternal(state, action);
}



export function newsReducerOld(state = initialState, action: newsAction.Actions): NewsState {
    switch (action.type) {

        case newsAction.recieveGroupJoinedAction:
            return Object.assign({}, state, {
                news: {
                    newsItems: state.news.newsItems,
                    groups: (state.news.groups.indexOf(action.group) > -1) ? state.news.groups : state.news.groups.concat(action.group)
                }
            });

        case newsAction.recieveNewsItemAction:
            return Object.assign({}, state, {
                news: {
                    newsItems: state.news.newsItems.concat(action.newsItem),
                    groups: state.news.groups
                }
            });

        case newsAction.recieveNewsGroupHistoryAction:
            return Object.assign({}, state, {
                news: {
                    newsItems: action.newsItems,
                    groups: state.news.groups
                }
            });

        case newsAction.recieveGroupLeftAction:
            const data = [];
            for (const entry of state.news.groups) {
                if (entry !== action.group) {
                    data.push(entry);
                }
            }
            console.log(data);
            return Object.assign({}, state, {
                news: {
                    newsItems: state.news.newsItems,
                    groups: data
                }
            });

        case newsAction.selectAllNewsGroupsFinishedAction:
            return Object.assign({}, state, {
                news: {
                    newsItems: state.news.newsItems,
                    groups: action.groups
                }
            });

        default:
            return state;

    }
}
