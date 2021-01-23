import { NewsState } from './news.state';
import { createFeatureSelector, createSelector } from '@ngrx/store';

export const newsStoreName = 'news';

export const selectWorldStore = createFeatureSelector(newsStoreName);
