import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from '../../../shared/services/api.service';
import { AccountDetails } from '../interfaces/account-details.interface';

@Injectable({
  providedIn: 'root'
})
export class AccountService extends ApiService {
  private readonly accountUrl = `${this.apiUrl}/api/account`;

  getAccountDetails(): Observable<AccountDetails> {
    return this.http.get<AccountDetails>(`${this.accountUrl}/details`, {
      withCredentials: true,
    });
  }
}
