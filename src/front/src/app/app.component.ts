import { Component } from '@angular/core';
import { Observable } from 'rxjs';

@Component({
  selector: 'bc-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  public showSpinner$: Observable<boolean> | undefined;
}
