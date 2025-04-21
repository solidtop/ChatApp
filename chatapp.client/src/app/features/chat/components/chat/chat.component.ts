import { AsyncPipe } from '@angular/common';
import { Component, effect, ElementRef, inject, OnDestroy, OnInit, viewChild, viewChildren } from '@angular/core';
import { AvatarComponent } from "../../../../shared/components/avatar/avatar.component";
import { MessageType } from '../../enums/message-type.enum';
import { ChannelMessage } from '../../interfaces/channel-message.interface';
import { ChatMessage } from '../../interfaces/chat-message.interface';
import { ChatHubService } from '../../services/chat-hub.service';
import { ChatStateService } from '../../services/chat-state.service';
import { ChannelInputComponent } from '../channel-input/channel-input.component';
import { ChatInputComponent } from '../chat-input/chat-input.component';

@Component({
  selector: 'app-chat',
  imports: [ChannelInputComponent, ChatInputComponent, AsyncPipe, AvatarComponent],
  templateUrl: './chat.component.html',
  styleUrl: './chat.component.css'
})
export class ChatComponent implements OnInit, OnDestroy {
  private readonly chatHubService = inject(ChatHubService);
  readonly chatState = inject(ChatStateService);

  scrollList = viewChild.required<ElementRef<HTMLUListElement>>('scrollList');
  scrollItems = viewChildren<ElementRef<HTMLLIElement>>('scrollItem');
  shouldForceScroll = true;

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
    this.chatState.load();
    await this.chatHubService.joinChannel(this.chatState.currentChannelId);
  }
  
  ngOnDestroy(): void {
    this.chatHubService.stopConnection();
  }

  async changeChannel(channelId: number): Promise<void> {
    await this.chatHubService.leaveChannel(this.chatState.currentChannelId);
    await this.chatHubService.joinChannel(channelId);
    this.chatState.setCurrentChannel(channelId);
    this.chatState.update();
    this.shouldForceScroll = true;
  }
  
  sendMessage(text: string): void {
    this.chatHubService.sendMessage({
      channelId: this.chatState.currentChannelId,
      text,
    });
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

  private scrollToBottom(): void {
    const list = this.scrollList().nativeElement;

    list.scroll({
      top: list.scrollHeight,
    });
  }

  isChannelMessage(message: ChatMessage): message is ChannelMessage {
    return message.type === MessageType.Channel;
  }
}
