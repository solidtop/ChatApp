import { effect, inject, Injectable, signal } from '@angular/core';
import { toSignal } from '@angular/core/rxjs-interop';
import { StorageService } from '../../../shared/services/storage.service';
import { ChatMessage } from '../interfaces/chat-message.interface';
import { ChatHubService } from './chat-hub.service';
import { ChatService } from './chat.service';

@Injectable({
  providedIn: 'root'
})
export class ChatStateService {
  private readonly chatService = inject(ChatService);
  private readonly chatHub = inject(ChatHubService);
  private readonly storage = inject(StorageService);

  public channels = toSignal(this.chatService.getChannels());
  public messages = signal<ChatMessage[]>([]);

  public currentChannelId = signal<number>(
    this.storage.getLocal<number>('currentChannelId') ?? 1
  );

  constructor() {
    effect(() => {
      this.chatService
      .getRecentMessages(this.currentChannelId())
      .subscribe((recentMessages) => this.messages.set(recentMessages));

      this.storage.setLocal('currentChannelId', this.currentChannelId());
    });

    effect(() => {
      this.chatHub.onReceiveMessage((incomingMessage) => {
        this.messages.update((currentMessages) => [...currentMessages, incomingMessage]);
      });
    });
  }
}
