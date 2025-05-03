import { Injectable } from '@angular/core';
import { HttpTransportType, HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { ApiService } from '../../../shared/services/api.service';
import { ChatMessage } from '../interfaces/chat-message.interface';

@Injectable({
  providedIn: 'root'
})
export class ChatHubService extends ApiService {
  private readonly connection: HubConnection; 
 
  constructor() {
    super();

    this.connection = new HubConnectionBuilder()
    .withUrl(`${this.apiUrl}/chathub`, {
      transport: HttpTransportType.WebSockets,
      skipNegotiation: true,
      withCredentials: true,
    })
    .build();
  }

  public async startConnection(): Promise<void> {
    return this.connection
      .start()
      .catch((err) => console.error('Error while starting connection: ', err));
  }

  public async stopConnection(): Promise<void> {
    return this.connection.stop();
  }

  public joinChannel(channelId: number): Promise<void> {
    return this.connection.invoke('JoinChannel', channelId);
  }

  public leaveChannel(channelId: number): Promise<void> {
    return this.connection.invoke('LeaveChannel', channelId);
  }

  public sendChannelMessage(channelId: number, content: string): Promise<void> {
    return this.connection.send('SendChannelMessage', channelId, content);
  } 

  public executeCommand(commandText: string): Promise<void> {
    return this.connection.send('ExecuteCommand', commandText);
  }

  public onReceiveMessage(callback: (message: ChatMessage) => void) {
    this.connection.on('ReceiveMessage', callback);
  }
}
