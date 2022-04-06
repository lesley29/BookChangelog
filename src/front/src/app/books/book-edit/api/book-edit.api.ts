import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Author } from 'src/app/core/models/author.model';
import { PagedResponse } from 'src/app/core/models/pagination.model';
import { ApiService } from 'src/app/core/services/api.service';
import { UpdateBookRequest } from '../models/update-book-request.model';

@Injectable()
export class BookEditApi {

  constructor(private readonly api: ApiService) { }

  public getAuthors(): Observable<PagedResponse<Author>> {
    return this.api.get<PagedResponse<Author>>("authors");
  }

  public updateBook(bookId: string, request: UpdateBookRequest): Observable<void> {
    return this.api.put<void>(`books/${bookId}`, request);
  }
}
