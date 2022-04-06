import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Book, BookChangeHistory } from 'src/app/core/models/book.model';
import { PagedResponse } from 'src/app/core/models/pagination.model';

@Injectable()
export class BookListState {
  private readonly books$ = new BehaviorSubject<Book[]>([]);
  private readonly totalBookCount$ = new BehaviorSubject<number>(0);
  private readonly changeHistory$ = new BehaviorSubject<BookChangeHistory[]>([]);
  private readonly changeHistoryCache = new Map<string, BookChangeHistory[]>();

  public getBooks(): Observable<Book[]> {
    return this.books$.asObservable();
  }

  public getTotalBookCount(): Observable<number> {
    return this.totalBookCount$.asObservable();
  }

  public getBookChangeHistory(): Observable<BookChangeHistory[]> {
    return this.changeHistory$.asObservable();
  }

  public setBooks(response: PagedResponse<Book>) {
    this.books$.next(response.items);
    this.totalBookCount$.next(response.totalCount);
  }

  public hasChangeHistory(bookId: string): boolean {
    return this.changeHistoryCache.has(bookId);
  }

  public addChangeHistory(bookId: string, changeHistory: BookChangeHistory[]) {
    this.changeHistoryCache.set(bookId, changeHistory);
    this.changeHistory$.next(changeHistory);
  }

  public reloadChangeHistory(bookId: string) {
    this.changeHistory$.next(this.changeHistoryCache.get(bookId)!);
  }
}
