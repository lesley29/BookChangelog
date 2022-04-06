import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { Author } from 'src/app/core/models/author.model';
import { BookEditApi } from './api/book-edit.api';
import { UpdateBookRequest } from './models/update-book-request.model';

@Injectable()
export class BookEditFacade {

  constructor(
    private readonly api: BookEditApi,
  ) { }

  // TODO: support paging
  public getAuthors(): Observable<Author[]> {
    return this.api.getAuthors()
      .pipe(
        map(r => r.items)
      );
  }

  public updateBook(bookId: string, request: UpdateBookRequest): Observable<void> {
    return this.api.updateBook(bookId, request);
  }
}
