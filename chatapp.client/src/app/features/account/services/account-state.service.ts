import { inject, Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { AccountDetails } from '../interfaces/account-details.interface';
import { AccountService } from './account.service';

export type AccountDetailsState = AccountDetails | null | undefined;

@Injectable({
  providedIn: 'root'
})
export class AccountStateService {
  private readonly accountService = inject(AccountService);
  private readonly detailsSubject = new BehaviorSubject<AccountDetailsState>(undefined);
  public readonly details$: Observable<AccountDetailsState> = this.detailsSubject.asObservable();

  loadDetails(): void {
    this.accountService.getAccountDetails().subscribe({ 
      next: (details: AccountDetails) => this.detailsSubject.next(details),
      error: () => this.detailsSubject.next(null),
    });
  }

  public getDetails(): AccountDetailsState {
    return this.detailsSubject.getValue();
  }

  public clear(): void {
    this.detailsSubject.next(null);
  }
}
