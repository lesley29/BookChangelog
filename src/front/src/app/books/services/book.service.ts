import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Book } from 'src/app/core/models/book.model';
import { ApiService } from 'src/app/core/services/api.service';

@Injectable()
export class BookService {

    constructor(private readonly apiService: ApiService) {
    }

    public get(id: string): Observable<Book> {
        return this.apiService.get<Book>(`books/${id}`);
    }
}