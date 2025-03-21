import { Injectable } from '@angular/core';
import { HttpTransportType, HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { BehaviorSubject } from 'rxjs';
import { ApiService } from '../../../shared/services/api.service';
import { ChatMessageRequest } from '../interfaces/chat-message-request.interface';
import { ChatMessageResponse } from '../interfaces/chat-message-response.interface';

@Injectable({
  providedIn: 'root'
})
export class ChatHubService extends ApiService {
 private readonly connection: HubConnection; 
 public readonly messages$ = new BehaviorSubject<ChatMessageResponse[]>([]);

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
      .then(() => this.addMessageListener())
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

  public sendMessage(request: ChatMessageRequest): Promise<void> {
    return this.connection.send('SendMessage', request);
  }

  private addMessageListener(): void {
    this.connection.on('ReceiveMessage', (message: ChatMessageResponse) => {
      const currentMessages = this.messages$.getValue();
      this.messages$.next([...currentMessages, message]);
    });  
  }
}
