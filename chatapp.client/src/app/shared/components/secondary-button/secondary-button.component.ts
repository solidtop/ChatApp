import { Component, input, output } from '@angular/core';

@Component({
  selector: 'app-secondary-button',
  imports: [],
  templateUrl: './secondary-button.component.html',
  styleUrl: './secondary-button.component.css'
})
export class SecondaryButtonComponent {
  type = input<'text' | 'submit' | 'reset'>('text');
  disabled = input<boolean>(false);
  click = output<MouseEvent>();
}
