import { Component, input } from '@angular/core';

@Component({
  selector: 'app-dropdown',
  imports: [],
  templateUrl: './dropdown.component.html',
  styleUrl: './dropdown.component.css'
})
export class DropdownComponent {
  readonly label = input<string>();
  // readonly icon = input<>();
  isOpen = false;

  toggle(): void {
    this.isOpen = !this.isOpen;
  }
}
