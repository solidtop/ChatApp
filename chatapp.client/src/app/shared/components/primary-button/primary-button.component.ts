import { Component, input, output } from '@angular/core';

@Component({
  selector: 'app-primary-button',
  imports: [],
  templateUrl: './primary-button.component.html',
  styleUrl: './primary-button.component.css'
})
export class PrimaryButtonComponent {
  type = input<'text' | 'submit' | 'reset'>('text');
  disabled = input<boolean>(false);
  click = output<MouseEvent>();
}
