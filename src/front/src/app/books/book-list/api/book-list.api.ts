import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Book } from 'src/app/core/models/book.model';
import { PagedResponse } from 'src/app/core/models/pagination.model';
import { ApiService } from 'src/app/core/services/api.service';

@Injectable()
export class BookListApi {

  constructor(private readonly api: ApiService) { }

  public getAuthors(): Observable<PagedResponse<Book>> {
    return this.api.get<PagedResponse<Book>>("books");
  }
}
