import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from '../../../shared/services/api.service';
import { Avatar } from '../interfaces/avatar.interface';

@Injectable({
  providedIn: 'root'
})
export class AvatarService extends ApiService {
  private readonly avatarUrl = `${this.apiUrl}/api/avatars`;
  
  public getAvatars(): Observable<Avatar[]> {
    return this.http.get<Avatar[]>(`${this.avatarUrl}`, {
      withCredentials: true,
    });
  }

  public getAvatar(avatarId: number) : Observable<Avatar> {
      return this.http.get<Avatar>(`${this.avatarUrl}/${avatarId}`, {
      withCredentials: true,
    });
  }
}
