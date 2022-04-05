import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Book } from 'src/app/core/models/book.model';
import { BookListFacade } from '../../book-list.facade';
import { BookFilter, SortableField, SortDirection } from '../../models/book-list.model';

@Component({
    selector: 'bc-book-list',
    templateUrl: './book-list.component.html',
    styleUrls: ['./book-list.component.css']
})
export class BookListComponent implements OnInit {
    public books$: Observable<Book[]>;
    public filter: BookFilter = {
        publishedFrom: null,
        publishedTo: null,
        sortBy: SortableField.Title,
        sortDirection: SortDirection.Ascending
    };
    
    constructor(
        private readonly bookListFacade: BookListFacade
    ) { 
        this.books$ = this.bookListFacade.getBooks();
    }
        
    ngOnInit(): void {
        this.bookListFacade.loadBooks(this.filter);
    }
    
    public onFilterChange(filter: BookFilter) {
        this.filter = filter;
        this.bookListFacade.loadBooks(this.filter);
    }
}
    