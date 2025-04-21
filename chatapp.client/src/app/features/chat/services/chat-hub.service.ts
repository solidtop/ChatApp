import { Injectable } from '@angular/core';
import { HttpTransportType, HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { Subject } from 'rxjs';
import { ApiService } from '../../../shared/services/api.service';
import { ChatMessageRequest } from '../interfaces/chat-message-request.interface';
import { ChatMessage } from '../interfaces/chat-message.interface';

@Injectable({
  providedIn: 'root'
})
export class ChatHubService extends ApiService {
  private readonly connection: HubConnection; 
  private newMessageSubject = new Subject<ChatMessage>();
  public newMessage$ = this.newMessageSubject.asObservable();

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

  public executeCommand(commandText: string): Promise<void> {
    return this.connection.send('ExecuteCommand', commandText);
  }

  private addMessageListener(): void {
    this.connection.on('ReceiveMessage', (message: ChatMessage) => {
      console.log(message);
      this.newMessageSubject.next(message);
    });  
  }
}
