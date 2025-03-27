import { inject, Injectable } from '@angular/core';
import { concat, map, Observable, scan } from 'rxjs';
import { StorageService } from '../../../shared/services/storage.service';
import { ChatChannel } from '../interfaces/chat-channel.interface';
import { ChatMessageResponse } from '../interfaces/chat-message-response.interface';
import { ChatHubService } from './chat-hub.service';
import { ChatService } from './chat.service';

@Injectable({
  providedIn: 'root'
})
export class ChatStateService {
  private readonly chatService = inject(ChatService);
  private readonly chatHubService = inject(ChatHubService);
  private readonly storageService = inject(StorageService);

  public channels$!: Observable<ChatChannel[]>;
  public messages$!: Observable<ChatMessageResponse[]>;
  public currentChannelId!: number;

  public load(): void {
    this.channels$ = this.chatService.getChannels();
    this.loadCurrentChannel();
    this.loadMessages();
  }

  public update(): void {
    this.loadMessages();
  }

  public setCurrentChannel(channelId: number): void {
    this.currentChannelId = channelId;
    this.storageService.setLocal('currentChannelId', channelId);
  } 

  private loadMessages(): void {
    const initialMessages$ = this.chatService.getLatestMessages(this.currentChannelId);

    const newMessages$ = this.chatHubService.newMessage$.pipe(
      map((message) => [message])
    );
  
    this.messages$ = concat(initialMessages$, newMessages$).pipe(
      scan((allMessages, newMessages) => [...allMessages, ...newMessages])
    );
  }

  private loadCurrentChannel(): void {
    const channelId = this.storageService.getLocal<number>('currentChannelId');
    this.currentChannelId = channelId || 1;
  } 
}
