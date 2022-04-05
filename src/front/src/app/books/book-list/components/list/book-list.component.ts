import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Book } from 'src/app/core/models/book.model';
import { BookListFacade } from '../../book-list.facade';

@Component({
  selector: 'bc-book-list',
  templateUrl: './book-list.component.html',
  styleUrls: ['./book-list.component.css']
})
export class BookListComponent implements OnInit {

  public books$: Observable<Book[]>;

  constructor(
    private readonly bookListFacade: BookListFacade
  ) { 
    this.books$ = this.bookListFacade.getBooks();
  }

  ngOnInit(): void {
    this.bookListFacade.loadBooks();
  }

}
