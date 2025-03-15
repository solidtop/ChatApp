import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { first, map } from 'rxjs';
import { AccountStateService } from '../../account/services/account-state.service';

export const authGuard: CanActivateFn = () => {
  const accountStateService = inject(AccountStateService);
  const router = inject(Router);

  return accountStateService.profile$.pipe(
    first((profile) => profile !== undefined),
    map((profile) => profile ? true : router.parseUrl('/login'))
  ); 
};
