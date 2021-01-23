import { NewsState } from './news.state';
import * as newsAction from './news.action';
import { createReducer, on, Action } from '@ngrx/store';

export const initialState: NewsState = {
  news: {
      newsItems: [],
      groups: ['IT', 'global', 'sport']
  }
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
  on(newsAction.recieveGroupJoinedAction, (state, { payload }) => {
	  const { news } = state;
	  const { newsItems, groups } = news;
      return {
        ...state,
        news: {
          newsItems,
          groups: [...groups, payload]
        }
      };
    }
  ),
  on(newsAction.recieveNewsItemAction, (state, { payload }) => {
	  const { news } = state;
	  const { newsItems, groups } = news;
      return {
        ...state,
        news: {
          newsItems: [...newsItems, payload],
          groups
        }
      };
    }
  ),
  on(newsAction.recieveNewsGroupHistoryAction, (state, { payload }) => {
	  const { news } = state;
	  const { newsItems, groups } = news;
      return {
        ...state,
        news: {
          newsItems: [...payload],
          groups
        }
      };
    }
  ),
);

export function newsReducer(
  state: NewsState | undefined,
  action: Action
): any {
  return newsReducerInternal(state, action);
}


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
