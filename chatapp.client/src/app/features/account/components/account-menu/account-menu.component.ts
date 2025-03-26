import { AsyncPipe } from '@angular/common';
import { Component, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { AvatarComponent } from '../../../../shared/components/avatar/avatar.component';
import { MenuOptionComponent } from '../../../../shared/components/menu-option/menu-option.component';
import { MenuComponent } from '../../../../shared/components/menu/menu.component';
import { AuthService } from '../../../auth/services/auth.service';
import { AccountStateService } from '../../services/account-state.service';

@Component({
  selector: 'app-account-menu',
  imports: [AsyncPipe, RouterLink, MenuComponent, MenuOptionComponent, AvatarComponent],
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
