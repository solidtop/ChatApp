import { inject, Injectable } from '@angular/core';
import { first, map, Observable } from 'rxjs';
import { ApiService } from '../../../shared/services/api.service';
import { AccountStateService } from '../../account/services/account-state.service';
import { LoginRequest } from '../interfaces/login-request.interface';
import { RegisterRequest } from '../interfaces/register-request.interface';
import { RegisterResponse } from '../interfaces/register-response.interface';


@Injectable({
  providedIn: 'root'
})
export class AuthService extends ApiService {
  private readonly accountStateService = inject(AccountStateService);
  private readonly authUrl = `${this.apiUrl}/auth`;

  public register(request: RegisterRequest): Observable<RegisterResponse> {
    return this.http.post<RegisterResponse>(`${this.authUrl}/register`, request, {
      withCredentials: true
    });
  }

  public login(request: LoginRequest): Observable<void> {
    return this.http.post<void>(`${this.authUrl}/login`, request, {
      withCredentials: true,
    });
  }

  public logout(): Observable<void> {
    return this.http.post<void>(`${this.authUrl}/logout`, null, {
      withCredentials: true,
    });
  }

  public isAuthenticated(): Observable<boolean> {
      return this.accountStateService.profile$.pipe(
        first((profile) => profile !== undefined),
        map((profile) => profile ? true : false)); 
  }
}
