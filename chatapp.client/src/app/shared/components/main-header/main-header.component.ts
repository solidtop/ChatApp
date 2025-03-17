import { Component } from '@angular/core';
import { AccountMenuComponent } from '../../../features/account/components/account-menu/account-menu.component';

@Component({
  selector: 'app-main-header',
  imports: [AccountMenuComponent],
  templateUrl: './main-header.component.html',
  styleUrl: './main-header.component.css'
})
export class MainHeaderComponent {

}
