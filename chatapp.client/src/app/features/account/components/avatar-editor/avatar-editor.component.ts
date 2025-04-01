import { AsyncPipe } from '@angular/common';
import { Component, inject, input, linkedSignal, output } from '@angular/core';
import { Avatar } from '../../interfaces/avatar.interface';
import { AccountService } from '../../services/account.service';
import { AvatarService } from '../../services/avatar.service';

@Component({
  selector: 'app-avatar-editor',
  imports: [AsyncPipe],
  templateUrl: './avatar-editor.component.html',
  styleUrl: './avatar-editor.component.css'
})
export class AvatarEditorComponent {
  private readonly avatarService = inject(AvatarService);
  private readonly accountService = inject(AccountService);
  avatars$ = this.avatarService.getAvatars();

  currentAvatar = input<Avatar>();
  selectedAvatar = linkedSignal(() => this.currentAvatar());
  avatarUpdated = output<void>();

  onSubmit(ev: Event): void {
    ev.preventDefault();

    const avatar = this.selectedAvatar();
    
    this.accountService.updateAvatar(avatar!.id).subscribe({
      complete: () => this.avatarUpdated.emit(),
    });
  }
}
