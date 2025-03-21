import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from '../../../shared/services/api.service';
import { ChatChannel } from '../interfaces/chat-channel.interface';
import { ChatMessageResponse } from '../interfaces/chat-message-response.interface';

@Injectable({
  providedIn: 'root'
})
export class ChatService extends ApiService {
  private readonly chatUrl = `${this.apiUrl}/api/chat`;

  public getChannels(): Observable<ChatChannel[]> {
    return this.http.get<ChatChannel[]>(`${this.chatUrl}/channels`, {
      withCredentials: true,
    });
  }

  public getLatestMessages(channelId: number) : Observable<ChatMessageResponse[]> {
     return this.http.get<ChatMessageResponse[]>(`${this.chatUrl}/channels/${channelId}/messages?count=10`, {
      withCredentials: true,
    });
  }
}
