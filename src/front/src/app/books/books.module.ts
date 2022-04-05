import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BookEditComponent } from './book-edit/book-edit.component';
import { BookAddComponent } from './book-add/book-add.component';
import { BooksRoutingModule } from './books-routing.module';
import { BookListComponent } from './book-list/components/list/book-list.component';
import { BookListApi } from './book-list/api/book-list.api';
import { BookListState } from './book-list/state/book-list.state';
import { BookListFacade } from './book-list/book-list.facade';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatListModule } from '@angular/material/list';

@NgModule({
  declarations: [
    BookListComponent,
    BookEditComponent,
    BookAddComponent
  ],
  imports: [
    CommonModule,
    BooksRoutingModule,
    MatExpansionModule,
    MatListModule
  ],
  providers: [
    BookListApi,
    BookListState,
    BookListFacade
  ]
})
export class BooksModule { }
