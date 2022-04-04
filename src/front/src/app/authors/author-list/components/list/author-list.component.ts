import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Author } from 'src/app/core/models/author.model';
import { AuthorListFacade } from '../../author-list.facade';

@Component({
  selector: 'bc-author-list',
  templateUrl: './author-list.component.html',
  styleUrls: ['./author-list.component.css']
})
export class AuthorListComponent implements OnInit {

  public authors$: Observable<Author[]>;

  constructor(
    private readonly authorListFacade: AuthorListFacade
  ) { 
    this.authors$ = this.authorListFacade.getAuthors();
  }

  ngOnInit(): void {
    this.authorListFacade.loadAuthors();
  }
}
