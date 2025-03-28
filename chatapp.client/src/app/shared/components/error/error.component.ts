import { Component } from '@angular/core';

@Component({
  selector: 'app-error',
  imports: [],
  template: `<span class="error"><ng-content /></span>`,
  styles: `.error { color: var(--error-color) }`
})
export class ErrorComponent {

}
