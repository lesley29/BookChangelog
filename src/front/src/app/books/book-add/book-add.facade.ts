import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { Author } from 'src/app/core/models/author.model';
import { BookAddApi } from './api/book-add.api';
import { CreateBookRequest } from './models/create-book-request.model';

@Injectable()
export class BookAddFacade {

  constructor(
    private readonly api: BookAddApi,
  ) { }

  // TODO: support paging
  public getAuthors(): Observable<Author[]> {
    return this.api.getAuthors()
      .pipe(
        map(r => r.items)
      );
  }

  public addBook(request: CreateBookRequest): Observable<void> {
    return this.api.addBook(request)
      .pipe(
        map(_ => {})
      );
  }
}
