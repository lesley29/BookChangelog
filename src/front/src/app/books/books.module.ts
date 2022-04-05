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
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { ReactiveFormsModule } from '@angular/forms';
import { BookListFilterComponent } from './book-list/components/filter/book-list-filter.component';
import { MatNativeDateModule } from '@angular/material/core';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatButtonToggleModule } from '@angular/material/button-toggle';

@NgModule({
  declarations: [
    BookListComponent,
    BookEditComponent,
    BookAddComponent,
    BookListFilterComponent
  ],
  imports: [
    CommonModule,
    BooksRoutingModule,
    MatExpansionModule,
    MatListModule,
    MatFormFieldModule,
    MatDatepickerModule,
    ReactiveFormsModule,
    MatNativeDateModule,
    MatInputModule,
    MatButtonModule,
    MatButtonToggleModule
  ],
  providers: [
    BookListApi,
    BookListState,
    BookListFacade
  ]
})
export class BooksModule { }
