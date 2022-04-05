import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Book } from 'src/app/core/models/book.model';
import { BookListApi } from './api/book-list.api';
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

public loadBooks() {
  this.api.getAuthors()
    .subscribe(books => {
        this.state.setBooks(books);
    });
  }
}
