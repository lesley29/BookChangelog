import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BookAddComponent } from './book-add/components/add/book-add.component';
import { BookEditComponent } from './book-edit/book-edit.component';
import { BookListComponent } from './book-list/components/list/book-list.component';

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
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class BooksRoutingModule { }