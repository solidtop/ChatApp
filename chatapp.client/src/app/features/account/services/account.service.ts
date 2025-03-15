import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from '../../../shared/services/api.service';
import { AccountProfile } from '../interfaces/account-profile.interface';

@Injectable({
  providedIn: 'root'
})
export class AccountService extends ApiService {
  private readonly accountUrl = `${this.apiUrl}/account`;

  getAccountProfile(): Observable<AccountProfile> {
    return this.http.get<AccountProfile>(`${this.accountUrl}/profile`, {
      withCredentials: true,
    });
  }
}
