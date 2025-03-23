import { AsyncPipe } from '@angular/common';
import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { ChatHubService } from '../../services/chat-hub.service';
import { ChatStateService } from '../../services/chat-state.service';
import { ChannelInputComponent } from '../channel-input/channel-input.component';
import { ChatInputComponent } from '../chat-input/chat-input.component';

@Component({
  selector: 'app-chat',
  imports: [ChannelInputComponent, ChatInputComponent, AsyncPipe],
  templateUrl: './chat.component.html',
  styleUrl: './chat.component.css'
})
export class ChatComponent implements OnInit, OnDestroy {
  private readonly chatHubService = inject(ChatHubService);
  public readonly chatState = inject(ChatStateService);

  async ngOnInit(): Promise<void> {
    await this.chatHubService.startConnection();
    this.chatState.load();
    await this.chatHubService.joinChannel(this.chatState.currentChannelId);
  }
  
  ngOnDestroy(): void {
    this.chatHubService.stopConnection();
  }

  async changeChannel(channelId: number): Promise<void> {
    await this.chatHubService.joinChannel(channelId);
    this.chatState.setCurrentChannel(channelId);
    this.chatState.update();
  }
  
  sendMessage(text: string): void {
    this.chatHubService.sendMessage({
      text,
      channelId: this.chatState.currentChannelId,
    });
  }
}
