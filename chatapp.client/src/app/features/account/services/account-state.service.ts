import { inject, Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { AccountProfile } from '../interfaces/account-profile.interface';
import { AccountService } from './account.service';

export type AccountProfileState = AccountProfile | null | undefined;

@Injectable({
  providedIn: 'root'
})
export class AccountStateService {
  private readonly accountService = inject(AccountService);
  private readonly profileSubject = new BehaviorSubject<AccountProfileState>(undefined);
  public readonly profile$: Observable<AccountProfileState> = this.profileSubject.asObservable();

  loadProfile(): void {
    this.accountService.getAccountProfile().subscribe({ 
      next: (profile: AccountProfile) => this.profileSubject.next(profile),
      error: () => this.profileSubject.next(null),
    });
  }

  public getProfile(): AccountProfileState {
    return this.profileSubject.getValue();
  }

  public clearProfile(): void {
    this.profileSubject.next(null);
  }
}
