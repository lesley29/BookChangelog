import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Book, BookChangeHistory } from 'src/app/core/models/book.model';
import { BookListApi } from './api/book-list.api';
import { BookFilter } from './models/book-list.model';
import { BookListState } from './state/book-list.state';

@Injectable()
export class BookListFacade {

  constructor (
    private readonly api: BookListApi,
    private readonly state: BookListState
  ) {
  }

public getBooks(): Observable<Book[]> {
  return this.state.getBooks();
}

public getTotalBookCount(): Observable<number> {
  return this.state.getTotalBookCount();
}

public getBookChangeHistory(): Observable<BookChangeHistory[]> {
  return this.state.getBookChangeHistory();
}

public loadBookChangeHistory(bookId: string) {
  if (this.state.hasChangeHistory(bookId)) {
    this.state.reloadChangeHistory(bookId);
  } else {
    this.api.getBookChangeHistory(bookId)
      .subscribe(changeHistory => {
        this.state.addChangeHistory(bookId, changeHistory);
      });
  }
}

public loadBooks(pageIndex: number, pageSize: number, filter: BookFilter) {
  this.api.getBooks(pageIndex, pageSize, filter)
    .subscribe(books => {
        this.state.setBooks(books);
    });
  }
}
