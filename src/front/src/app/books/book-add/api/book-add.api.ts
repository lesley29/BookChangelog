import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Author } from 'src/app/core/models/author.model';
import { Book } from 'src/app/core/models/book.model';
import { PagedResponse } from 'src/app/core/models/pagination.model';
import { ApiService } from 'src/app/core/services/api.service';
import { CreateBookRequest } from '../models/create-book-request.model';

@Injectable()
export class BookAddApi {

  constructor(private readonly api: ApiService) { }

  public getAuthors(): Observable<PagedResponse<Author>> {
    return this.api.get<PagedResponse<Author>>("authors");
  }

  public addBook(request: CreateBookRequest): Observable<Book> {
    return this.api.post<Book>("books", request);
  }
}
