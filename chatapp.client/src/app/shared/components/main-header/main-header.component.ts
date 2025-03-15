import { Component } from '@angular/core';
import { AccountProfileComponent } from '../account-profile/account-profile.component';
import { MainNavbarComponent } from '../main-navbar/main-navbar.component';

@Component({
  selector: 'app-main-header',
  imports: [MainNavbarComponent, AccountProfileComponent],
  templateUrl: './main-header.component.html',
  styleUrl: './main-header.component.css'
})
export class MainHeaderComponent {

}
