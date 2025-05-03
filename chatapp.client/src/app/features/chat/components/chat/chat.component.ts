import { Component, effect, ElementRef, inject, OnDestroy, OnInit, viewChild, viewChildren } from '@angular/core';
import { AvatarComponent } from "../../../../shared/components/avatar/avatar.component";
import { MessageType } from '../../interfaces/chat-message.interface';
import { ChatHubService } from '../../services/chat-hub.service';
import { ChatStateService } from '../../services/chat-state.service';
import { ChannelInputComponent } from '../channel-input/channel-input.component';
import { ChatInputComponent } from '../chat-input/chat-input.component';

@Component({
  selector: 'app-chat',
  imports: [ChannelInputComponent, ChatInputComponent, AvatarComponent],
  templateUrl: './chat.component.html',
  styleUrl: './chat.component.css'
})
export class ChatComponent implements OnInit, OnDestroy {
  readonly chatHubService = inject(ChatHubService);
  readonly chatState = inject(ChatStateService);

  scrollList = viewChild.required<ElementRef<HTMLUListElement>>('scrollList');
  scrollItems = viewChildren<ElementRef<HTMLLIElement>>('scrollItem');
  shouldForceScroll = true;

  MessageType = MessageType;

  constructor() { 
    effect(() => {
      const items = this.scrollItems();

      if (items.length === 0) 
        return;

      if (this.isNearBottom() || this.shouldForceScroll) {
        this.scrollToBottom();
        this.shouldForceScroll = false;
      }
    });
  }

  async ngOnInit(): Promise<void> {
    await this.chatHubService.startConnection();
    await this.chatHubService.joinChannel(this.chatState.currentChannelId());
  }
  
  ngOnDestroy(): void {
    this.chatHubService.stopConnection();
  }

  async changeChannel(channelId: number): Promise<void> {
    await this.chatHubService.leaveChannel(this.chatState.currentChannelId());
    await this.chatHubService.joinChannel(channelId);
    this.chatState.currentChannelId.set(channelId);
    this.shouldForceScroll = true;
  }
  
  sendChannelMessage(content: string): void {
    this.chatHubService.sendChannelMessage(
      this.chatState.currentChannelId(),
      content,
    );
  }

  executeCommand(commandText: string) {
    this.chatHubService.executeCommand(commandText);
  }

  isNearBottom(): boolean {
    const list = this.scrollList().nativeElement;
    const threshhold = 150;
    const position = list.scrollTop + list.offsetHeight;
    return position > list.scrollHeight - threshhold;
  }

  getTypeName(type: MessageType): string {
    switch (type) {
      case MessageType.Channel: return 'channel';
      case MessageType.Whisper: return 'whisper';
      case MessageType.Notification: return 'notification';
      case MessageType.Announcement: return 'announcement';
      case MessageType.Error: return 'error';
    }
  }

  private scrollToBottom(): void {
    const list = this.scrollList().nativeElement;

    list.scroll({
      top: list.scrollHeight,
    });
  }
}
