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
  currentChannel = input<ChatChannel>();
  channelChanged = output<ChatChannel>();

  changeChannel(channel: ChatChannel): void {
    this.channelChanged.emit(channel);
  }

  isCurrentChannel(channel: ChatChannel): boolean {
    return channel.id === this.currentChannel()?.id;
  }
}
