import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Author } from 'src/app/core/models/author.model';
import { AuthorListApi } from './api/author-list.api';
import { AuthorListState } from './state/author-list.state';

@Injectable()
export class AuthorListFacade {

  constructor (
      private readonly api: AuthorListApi,
      private readonly state: AuthorListState
  ) {
  }

  public getAuthors(): Observable<Author[]> {
    return this.state.getAuthors();
  }

  public loadAuthors() {
      this.api.getAuthors()
          .subscribe(authors => {
              this.state.setAuthors(authors);
          });
  }
}
