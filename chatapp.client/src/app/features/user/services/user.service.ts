import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from '../../../shared/services/api.service';
import { UserProfile } from '../interfaces/user-profile.interface';
import { UserSummary } from '../interfaces/user-summary.interface';

@Injectable({
  providedIn: 'root'
})
export class UserService extends ApiService {
  private readonly userUrl = `${this.apiUrl}/users`;

  getUsers(): Observable<UserSummary[]> {
    return this.http.get<UserSummary[]>(this.userUrl, {
      withCredentials: true,
    });
  }

  getUser(id: string): Observable<UserProfile> {
    return this.http.get<UserProfile>(`${this.userUrl}/${id}`, {
      withCredentials: true,
    });
  }
}
