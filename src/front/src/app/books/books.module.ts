import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BookListComponent } from './book-list/book-list.component';
import { BookEditComponent } from './book-edit/book-edit.component';

@NgModule({
  declarations: [
    BookListComponent,
    BookEditComponent
  ],
  imports: [
    CommonModule
  ]
})
export class BooksModule { }
