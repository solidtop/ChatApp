import { AsyncPipe } from '@angular/common';
import { Component, inject } from '@angular/core';
import { MainHeaderComponent } from '../../../shared/components/main-header/main-header.component';
import { AuthService } from '../../auth/services/auth.service';
import { ChatComponent } from '../../chat/components/chat/chat.component';

@Component({
  selector: 'app-home-page',
  imports: [MainHeaderComponent, ChatComponent, AsyncPipe],
  templateUrl: './home-page.component.html',
  styleUrl: './home-page.component.css'
})
export class HomePageComponent {
  readonly authService = inject(AuthService);
}
