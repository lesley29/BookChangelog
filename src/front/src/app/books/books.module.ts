import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
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
import { MatPaginatorModule } from '@angular/material/paginator';
import { ChangeHistoryComponent } from './book-list/components/change-history/change-history.component';
import { MatDividerModule } from '@angular/material/divider';
import { MatSelectModule } from '@angular/material/select';
import { BookAddComponent } from './book-add/components/add/book-add.component';
import { BookAddApi } from './book-add/api/book-add.api';
import { BookAddFacade } from './book-add/book-add.facade';
import { MatIconModule } from '@angular/material/icon';
import { BookEditComponent } from './book-edit/components/edit/book-edit.component';
import { BookService } from './services/book.service';
import { BookResolver } from './services/book.resolver';
import { BookEditApi } from './book-edit/api/book-edit.api';
import { BookEditFacade } from './book-edit/book-edit.facade';

@NgModule({
  declarations: [
    BookListComponent,
    BookEditComponent,
    BookAddComponent,
    BookListFilterComponent,
    ChangeHistoryComponent
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
    MatButtonToggleModule,
    MatPaginatorModule,
    MatDividerModule,
    MatSelectModule,
    MatIconModule
  ],
  providers: [
    BookListApi,
    BookListState,
    BookListFacade,
    BookAddApi,
    BookAddFacade,
    BookService,
    BookResolver,
    BookEditApi,
    BookEditFacade
  ]
})
export class BooksModule { }
