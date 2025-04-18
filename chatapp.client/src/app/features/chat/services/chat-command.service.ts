import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ChatCommandService {
  
  public isCommand(text: string): boolean {
    if (!text.startsWith('/')) {
      return false;
    }

    const parts = text.substring(1).split(' ');

    if (parts.length === 0) {
      return false;
    }

    return true;
  }
}
