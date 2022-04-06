import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Author } from 'src/app/core/models/author.model';
import { BookEditFacade } from '../../book-edit.facade';
import { UpdateBookRequest } from '../../models/update-book-request.model';

@Component({
  selector: 'bc-book-edit',
  templateUrl: './book-edit.component.html',
  styleUrls: ['./book-edit.component.css']
})
export class BookEditComponent implements OnInit {
  private bookId!: string;
  public form: FormGroup = this.createForm();
  public authorList$: Observable<Author[]> = this.bookEditFacade.getAuthors();

  constructor(
    private readonly formBuilder: FormBuilder,
    private readonly bookEditFacade: BookEditFacade,
    private readonly router: Router,
    private readonly route: ActivatedRoute,
  ) { }

  ngOnInit(): void {
    const book = this.route.snapshot.data["book"];
    this.bookId = book.id;
    const authors: Author[] = book.authors;

    this.form.patchValue({
      title: book.title,
      description: book.description,
      publicationDate: book.publicationDate,
      authors: authors.map(a => a.id)
    });
  }

  public onSubmit(): void {
    const request = this.formValueToCreateRequest();
    this.bookEditFacade.updateBook(this.bookId, request)
      .subscribe(() => {
        this.router.navigate(['/books']);
      });
  }

  private formValueToCreateRequest(): UpdateBookRequest {
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
