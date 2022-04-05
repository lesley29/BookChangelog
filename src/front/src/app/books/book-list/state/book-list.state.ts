import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Book } from 'src/app/core/models/book.model';
import { PagedResponse } from 'src/app/core/models/pagination.model';

@Injectable()
export class BookListState {
  private readonly books$ = new BehaviorSubject<Book[]>([]);

  public getBooks(): Observable<Book[]> {
    return this.books$.asObservable();
  }

  public setBooks(response: PagedResponse<Book>) {
    this.books$.next(response.items);
  }
}
