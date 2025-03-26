import { Component } from '@angular/core';

@Component({
  selector: 'app-menu-option',
  imports: [],
  template: `<li class="menu__option"><ng-content /></li>`,
  styleUrl: './menu-option.component.css'
})
export class MenuOptionComponent {
}
