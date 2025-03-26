import { Component, input, output } from '@angular/core';

@Component({
  selector: 'app-button',
  imports: [],
  template: `<button [class]="'button ' + variant()" [type]="type()" [disabled]="disabled()" (click)="click"><ng-content /></button>`,
  styleUrl: './button.component.css'
})
export class ButtonComponent {
  type = input<'text' | 'submit' | 'reset'>('text');
  variant = input<'text' | 'primary' | 'secondary'>('primary');
  disabled = input<boolean>(false);
  click = output<MouseEvent>();
}
