import { Component, input, output } from '@angular/core';
import { ChatChannel } from '../../interfaces/chat-channel.interface';

@Component({
  selector: 'app-channel-input',
  imports: [],
  templateUrl: './channel-input.component.html',
  styleUrl: './channel-input.component.css'
})
export class ChannelInputComponent {
  channels = input<ChatChannel[]>();
  currentChannelId = input<number>();
  channelChanged = output<number>();

  changeChannel(channel: ChatChannel): void {
    this.channelChanged.emit(channel.id);
  }

  isCurrentChannel(channel: ChatChannel): boolean {
    return channel.id === this.currentChannelId();
  }
}
