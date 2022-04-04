import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { AuthorListComponent } from './author-list/components/list/author-list.component';
import { AuthorListApi } from './author-list/api/author-list.api';
import { AuthorListState } from './author-list/state/author-list.state';
import { AuthorListFacade } from './author-list/author-list.facade';
import { AuthorsRoutingModule } from './authors-routing.module';

@NgModule({
  declarations: [
    AuthorListComponent
  ],
  imports: [
    CommonModule,
    AuthorsRoutingModule,
    MatCardModule,
  ],
  providers: [
    AuthorListApi,
    AuthorListState,
    AuthorListFacade
  ]
})
export class AuthorsModule { }
