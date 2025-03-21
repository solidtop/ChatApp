import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { take } from 'rxjs';
import { StorageService } from '../../../../shared/services/storage.service';
import { ChatChannel } from '../../interfaces/chat-channel.interface';
import { ChatHubService } from '../../services/chat-hub.service';
import { ChatService } from '../../services/chat.service';
import { ChannelInputComponent } from '../channel-input/channel-input.component';

@Component({
  selector: 'app-chat',
  imports: [ChannelInputComponent],
  templateUrl: './chat.component.html',
  styleUrl: './chat.component.css'
})
export class ChatComponent implements OnInit, OnDestroy {
  private readonly chatService = inject(ChatService);
  private readonly chatHubService = inject(ChatHubService);
  private readonly storageService = inject(StorageService);

  channels: ChatChannel[] = [];
  currentChannel?: ChatChannel;

  ngOnInit(): void {
    this.chatHubService.startConnection()
      .then(() => this.loadChannels());
  }

  ngOnDestroy(): void {
    this.chatHubService.stopConnection();
  }

  private loadChannels(): void {
    this.chatService.getChannels().pipe(take(1)).subscribe((channels) => {
      this.channels = channels;
      this.currentChannel = this.storageService.getLocal('channel-input') || channels[0];
      this.chatHubService.joinChannel(this.currentChannel.id);
    });
  }

  changeChannel(channel: ChatChannel): void {
    this.currentChannel = channel;
    this.storageService.setLocal('channel-input', this.currentChannel);
    this.chatHubService.joinChannel(channel.id);
  }
}
