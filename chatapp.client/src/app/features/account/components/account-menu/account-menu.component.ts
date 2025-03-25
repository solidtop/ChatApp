import { AsyncPipe } from '@angular/common';
import { Component, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { DropdownOptionComponent } from '../../../../shared/components/dropdown-option/dropdown-option.component';
import { DropdownComponent } from '../../../../shared/components/dropdown/dropdown.component';
import { IconComponent } from '../../../../shared/components/icon/icon.component';
import { AuthService } from '../../../auth/services/auth.service';
import { AccountStateService } from '../../services/account-state.service';

@Component({
  selector: 'app-account-menu',
  imports: [AsyncPipe, RouterLink, DropdownComponent, DropdownOptionComponent, IconComponent],
  templateUrl: './account-menu.component.html',
  styleUrl: './account-menu.component.css'
})
export class AccountMenuComponent {
  private readonly authService = inject(AuthService); 
  readonly accountState = inject(AccountStateService);

  logout(): void {
    this.authService.logout() 
      .subscribe({
        error: (err) => console.error(err),
        complete: () => this.accountState.clear(),
      });
  }
}
