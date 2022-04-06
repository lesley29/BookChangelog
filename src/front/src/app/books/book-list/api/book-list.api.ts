import { HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Book, BookChangeHistory } from 'src/app/core/models/book.model';
import { PagedResponse } from 'src/app/core/models/pagination.model';
import { ApiService } from 'src/app/core/services/api.service';
import { BookFilter, SortDirection } from '../models/book-list.model';

@Injectable()
export class BookListApi {
    
    constructor(private readonly api: ApiService) { }
    
    public getBooks(pageNumber: number, pageSize: number, filter: BookFilter)
        : Observable<PagedResponse<Book>> 
    {
        let params = new HttpParams()
            .set('pageSize', pageSize.toString())
            .set('pageNumber', pageNumber.toString())
            .set('sortBy', filter.sortBy)
            .set('sortDirection', filter.sortDirection == SortDirection.Ascending ? 'asc' : 'desc');
        
        if (filter.publishedFrom) {
            params = params.append('publishedFrom', this.getDatePartAsString(filter.publishedFrom));
        }
        
        if (filter.publishedTo) {
            params = params.append('publishedTo', this.getDatePartAsString(filter.publishedTo));
        }
        
        return this.api.get<PagedResponse<Book>>("books", params);
    }

    public getBookChangeHistory(bookId: string): Observable<BookChangeHistory[]>{
        return this.api.get<BookChangeHistory[]>(`books/${bookId}/change-history`);
    }

    private getDatePartAsString(dateString: string): string {
        const date = new Date(dateString);

        return date.toISOString().substring(0, 10);
    }
}
