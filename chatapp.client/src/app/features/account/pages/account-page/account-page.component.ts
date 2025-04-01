import { AsyncPipe } from '@angular/common';
import { Component, inject } from '@angular/core';
import { MainHeaderComponent } from "../../../../shared/components/main-header/main-header.component";
import { AvatarEditorComponent } from '../../components/avatar-editor/avatar-editor.component';
import { DisplayColorEditorComponent } from '../../components/display-color-editor/display-color-editor.component';
import { AccountStateService } from '../../services/account-state.service';

@Component({
  selector: 'app-account-page',
  imports: [AsyncPipe, MainHeaderComponent, AvatarEditorComponent, DisplayColorEditorComponent],
  templateUrl: './account-page.component.html',
  styleUrl: './account-page.component.css'
})
export class AccountPageComponent {
  readonly accountState = inject(AccountStateService);
}
