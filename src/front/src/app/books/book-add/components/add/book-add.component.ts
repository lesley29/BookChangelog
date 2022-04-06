import { Component } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Author } from 'src/app/core/models/author.model';
import { BookAddFacade } from '../../book-add.facade';
import { CreateBookRequest } from '../../models/create-book-request.model';

@Component({
  selector: 'bc-book-add',
  templateUrl: './book-add.component.html',
  styleUrls: ['./book-add.component.css']
})
export class BookAddComponent {
  public form: FormGroup = this.createForm();
  public authorList$: Observable<Author[]> = this.bookAddFacade.getAuthors();

  constructor(
    private readonly formBuilder: FormBuilder,
    private readonly bookAddFacade: BookAddFacade,
    private readonly router: Router,
  ) { 
  }

  public onSubmit(): void {
    const request = this.formValueToCreateRequest();
    this.bookAddFacade.addBook(request)
      .subscribe(() => {
        this.router.navigate(['/books']);
      });
  }

  private formValueToCreateRequest(): CreateBookRequest {
    return {
        title: this.form.get('title')!.value,
        description: this.form.get('description')?.value,
        publicationDate: this.getDatePartAsString(this.form.get('publicationDate')!.value),
        authors: this.form.get('authors')!.value,
    };
  }

  private getDatePartAsString(dateString: string): string {
    const date = new Date(dateString);

    return date.toISOString().substring(0, 10);
  }

  private createForm(): FormGroup {
    return this.formBuilder.group({
        title: this.formBuilder.control(''),
        description: this.formBuilder.control(''),
        publicationDate: this.formBuilder.control(''),
        authors: this.formBuilder.control([])
    });
  }
}
