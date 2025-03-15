import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { AccountStateService } from './features/account/services/account-state.service';

@Component({
  imports: [RouterOutlet],
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  private readonly accountStateService = inject(AccountStateService);

  ngOnInit(): void {
    this.accountStateService.loadProfile();
  }
}
