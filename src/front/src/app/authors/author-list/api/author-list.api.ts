import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Author } from 'src/app/core/models/author.model';
import { PagedResponse } from 'src/app/core/models/pagination.model';
import { ApiService } from 'src/app/core/services/api.service';

@Injectable()
export class AuthorListApi {

  constructor(private readonly api: ApiService) { }

  public getAuthors(): Observable<PagedResponse<Author>> {
    return this.api.get<PagedResponse<Author>>("authors");
  }
}
