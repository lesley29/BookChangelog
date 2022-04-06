import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BookAddComponent } from './book-add/components/add/book-add.component';
import { BookEditComponent } from './book-edit/components/edit/book-edit.component';
import { BookListComponent } from './book-list/components/list/book-list.component';
import { BookResolver } from './services/book.resolver';

const routes: Routes = [
    {
        path: '',
        component: BookListComponent,
    },
    {
        path: 'add',
        component: BookAddComponent,
    },
    {
        path: ':id/edit',
        component: BookEditComponent,
        resolve: {
            book: BookResolver
        }
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
    providers: [BookResolver]
})
export class BooksRoutingModule { }