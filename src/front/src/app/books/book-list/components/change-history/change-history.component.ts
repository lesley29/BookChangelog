import { Component, Input } from '@angular/core';
import { AuthorChange, BookAuthorChangeType, BookChangeHistory } from 'src/app/core/models/book.model';

@Component({
  selector: 'bc-change-history',
  templateUrl: './change-history.component.html',
  styleUrls: ['./change-history.component.css']
})
export class ChangeHistoryComponent {
  @Input()
  public changeHistory!: BookChangeHistory[];

  public authorChangeToString(change: AuthorChange): string {
    return change.changeType == BookAuthorChangeType.Added 
      ? change.name + " was added to authors"
      : change.name + " was removed from authors";
  }
}
