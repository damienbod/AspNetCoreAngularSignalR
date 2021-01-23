import { NewsState } from './news.state';
import * as newsAction from './news.action';
import { createReducer, on, Action } from '@ngrx/store';

export const initialState: NewsState = {
  newsItems: [],
  groups: ['IT', 'global', 'sport'],
};

// on(
//   YOUR_ACTION(S)_HERE,
//   (state, { payload }) => {
//   const { news } = state;
//   const { newsItems, groups } = news;
//     return {
//       ...state,
//       news: {
//     newsItems,
//     groups: [...groups, action.group]
//   }
//     };
//   }
// ),

// or

// on(
//   YOUR_ACTION(S)_HERE,
//   (state, { payload }) => {
//   const { news } = state;
//     return {
//       ...state,
//       news: {
//     newsItems: news.newsItems,
//     groups: [...news.groups, action.group]
//   }
//     };
//   }
// ),

const newsReducerInternal = createReducer(
  initialState,
  on(newsAction.recieveGroupJoinedAction, (state, { payload }) => {
    return {
      ...state,
      groups: [...state.groups, payload],
    };
  }),
  on(newsAction.recieveNewsItemAction, (state, { payload }) => {
    return {
      ...state,
      newsItems: [...state.newsItems, payload]
    };
  }),
  on(newsAction.recieveNewsGroupHistoryAction, (state, { payload }) => {
    return {
      ...state,
      newsItems: [...payload],
    };
  }),
  on(newsAction.selectAllNewsGroupsFinishedAction, (state, { payload }) => {
    return {
      ...state,
      groups: [...payload],
    };
  }),
  on(newsAction.recieveGroupLeftAction, (state, { payload }) => {
    const data = [];
    for (const entry of state.groups) {
      if (entry !== payload) {
        data.push(entry);
      }
    }
    console.log(data);

    return {
      ...state,
      groups: [...data],
    };
  })
);

export function newsReducer(state: NewsState | undefined, action: Action): any {
  return newsReducerInternal(state, action);
}
