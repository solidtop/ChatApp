import { AsyncPipe } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { AccountProfileState, AccountStateService } from '../../../features/account/services/account-state.service';

@Component({
  selector: 'app-account-profile',
  imports: [AsyncPipe],
  templateUrl: './account-profile.component.html',
  styleUrl: './account-profile.component.css'
})
export class AccountProfileComponent implements OnInit {
  private readonly accountStateService = inject(AccountStateService);
  profile$!: Observable<AccountProfileState>;

  ngOnInit(): void {
    this.profile$ = this.accountStateService.profile$;
  }
}
