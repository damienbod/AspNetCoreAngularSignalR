import * as newsAction from './../store/news.action';
import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { NewsState } from '../store/news.state';
import { NewsItem } from '../models/news-item';
import { Observable } from 'rxjs';

@Component({
    selector: 'app-news-component',
    templateUrl: './news.component.html'
})

export class NewsComponent implements OnInit {
    public async: any;
    newsItem: NewsItem;
    newsItems: NewsItem[] = [];
    group = 'IT';
    author = 'unknown';
    newsState$: Observable<NewsState>;
    groups = ['IT', 'global', 'sport'];

    constructor(private store: Store<any>) {

      this.newsState$ = this.store.select<NewsState>(state => state.news);

      this.store.select<NewsState>(state => state.news).subscribe((o: NewsState) => {
          this.newsItems = o.news.newsItems;
      });

      this.newsItem = new NewsItem();
      this.newsItem.AddData('', '', this.author, this.group);
    }

    public sendNewsItem(): void {
      this.newsItem.newsGroup = this.group;
      this.newsItem.author = this.author;
      this.store.dispatch(newsAction.sendNewsItemAction({payload:this.newsItem}));
    }

    public join(): void {
      this.store.dispatch(newsAction.joinGroupAction({payload:this.group}));
    }

    public leave(): void {
      this.store.dispatch(newsAction.leaveGroupAction({payload:this.group}));
    }

    ngOnInit() {
        console.log('go');
        this.store.dispatch(newsAction.selectAllNewsGroupsAction());
    }
}
