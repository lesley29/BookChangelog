import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { debounce, timer } from 'rxjs';
import { BookFilter, SortableField, SortDirection } from '../../models/book-list.model';

@Component({
  selector: 'bc-book-list-filter',
  templateUrl: './book-list-filter.component.html',
  styleUrls: ['./book-list-filter.component.css']
})
export class BookListFilterComponent implements OnInit {
  @Input()
  public filter!: BookFilter;

  @Output()
  public filterChange = new EventEmitter<BookFilter>();

  public filterForm!: FormGroup;

  public sortableFieldList = [
    SortableField.Title, 
    SortableField.PublicationDate
  ];

  public sortDirectionList = [
    SortDirection.Ascending, 
    SortDirection.Descending
  ];

  constructor(private readonly formBuilder: FormBuilder) {
    this.filterForm = this.formBuilder.group({
      "publishedFrom": this.formBuilder.control(null),
      "publishedTo": this.formBuilder.control(null),
      "sortBy": this.formBuilder.control(null),
      "sortDirection": this.formBuilder.control(null)
    });

    this.filterForm.valueChanges
      .pipe(
          debounce(() => timer(500))
      )
      .subscribe(value => {
          this.handleFormChange(value);
      });
  }

  public ngOnInit(): void {
    this.filterForm.patchValue({
      publishedFrom: this.filter.publishedFrom,
      publishedTo: this.filter.publishedTo,
      sortBy: this.filter.sortBy,
      sortDirection: this.filter.sortDirection
    }, {
      emitEvent: false
    });
  }

  private handleFormChange(newValue: any) {
    this.filterChange.emit({
      publishedFrom: newValue.publishedFrom,
      publishedTo: newValue.publishedTo,
      sortBy: newValue.sortBy,
      sortDirection: newValue.sortDirection
    });
  }
}
