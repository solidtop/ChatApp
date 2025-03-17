import { AsyncPipe } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { Observable, take } from 'rxjs';
import { AuthService } from '../../../auth/services/auth.service';
import { AccountDetailsState, AccountStateService } from '../../services/account-state.service';

@Component({
  selector: 'app-account-menu',
  imports: [AsyncPipe, RouterLink],
  templateUrl: './account-menu.component.html',
  styleUrl: './account-menu.component.css'
})
export class AccountMenuComponent implements OnInit {
  private readonly authService = inject(AuthService); 
  private readonly accountStateService = inject(AccountStateService);
  accountDetails$!: Observable<AccountDetailsState>;
  isOpen = false;

  ngOnInit(): void {
    this.accountDetails$ = this.accountStateService.details$;
  }

  toggle(): void {
    this.isOpen = !this.isOpen;
  }

  logout(): void {
    this.authService.logout() 
      .pipe(take(1))
      .subscribe({
        error: (err) => console.error(err),
        complete: () => {
          this.accountStateService.clear();
          this.isOpen = false;
        },
      });
  }
}
