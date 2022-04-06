import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { PageEvent } from '@angular/material/paginator';
import { Book } from 'src/app/core/models/book.model';
import { BookListFacade } from '../../book-list.facade';
import { BookFilter, SortableField, SortDirection } from '../../models/book-list.model';

@Component({
    selector: 'bc-book-list',
    templateUrl: './book-list.component.html',
    styleUrls: ['./book-list.component.css']
})
export class BookListComponent implements OnInit {
    public readonly defaultPageSize = 10;

    public books$: Observable<Book[]>;
    public totalBookCount$: Observable<number>;

    public pageSizes = [this.defaultPageSize, 20];

    public filter: BookFilter = {
        publishedFrom: null,
        publishedTo: null,
        sortBy: SortableField.Title,
        sortDirection: SortDirection.Ascending
    };

    private currentPageSize = this.defaultPageSize;
    
    constructor(
        private readonly bookListFacade: BookListFacade
    ) { 
        this.books$ = this.bookListFacade.getBooks();
        this.totalBookCount$ = this.bookListFacade.getTotalBookCount();
    }
        
    ngOnInit(): void {
        this.bookListFacade.loadBooks(0, this.currentPageSize, this.filter);
    }
    
    public onFilterChange(filter: BookFilter) {
        this.filter = filter;
        this.bookListFacade.loadBooks(0, this.currentPageSize, this.filter);
    }

    public onPageChange(event: PageEvent) {
        this.currentPageSize = event.pageSize;
        this.bookListFacade.loadBooks(event.pageIndex, event.pageSize, this.filter);
    }

}
    