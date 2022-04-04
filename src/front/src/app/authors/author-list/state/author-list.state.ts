import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Author } from 'src/app/core/models/author.model';
import { PagedResponse } from 'src/app/core/models/pagination.model';

@Injectable()
export class AuthorListState {
  private readonly authors$ = new BehaviorSubject<Author[]>([]);

  public getAuthors(): Observable<Author[]> {
    return this.authors$.asObservable();
  }

  public setAuthors(response: PagedResponse<Author>) {
    this.authors$.next(response.items);
  }
}
