import { Injectable } from '@angular/core';
import {
    Resolve,
    RouterStateSnapshot,
    ActivatedRouteSnapshot
} from '@angular/router';
import { Observable } from 'rxjs';
import { Book } from 'src/app/core/models/book.model';
import { BookService } from './book.service';

@Injectable()
export class BookResolver implements Resolve<Book> {

    constructor(private readonly bookService: BookService) {
    }

    resolve(route: ActivatedRouteSnapshot, _: RouterStateSnapshot): Observable<Book> {
        const bookId = route.paramMap.get("id");

        return this.bookService.get(bookId!);
    }
}