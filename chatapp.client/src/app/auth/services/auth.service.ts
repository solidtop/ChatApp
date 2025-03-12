import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from '../../shared/services/api.service';
import { LoginRequest } from '../interfaces/login-request.interface';
import { RegisterRequest } from '../interfaces/register-request.interface';
import { RegisterResponse } from '../interfaces/register-response.interface';

@Injectable({
  providedIn: 'root'
})
export class AuthService extends ApiService {
  private readonly authUrl = `${this.apiUrl}/api/auth`;

  public register(request: RegisterRequest): Observable<RegisterResponse> {
    return this.http.post<RegisterResponse>(`${this.authUrl}/register`, request, {
      headers: this.headers,
      withCredentials: true,
    });
  }

  public login(request: LoginRequest): Observable<void> {
    return this.http.post<void>(`${this.authUrl}/login`, request, {
      headers: this.headers,
      withCredentials: true,
    });
  }

  public logout(): Observable<void> {
    return this.http.post<void>(`${this.authUrl}/logout`, null, {
      withCredentials: true,
    });
  }
}
