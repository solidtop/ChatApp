import { Component, inject, input, linkedSignal, output } from '@angular/core';
import { AccountService } from '../../services/account.service';

@Component({
  selector: 'app-display-color-editor',
  imports: [],
  templateUrl: './display-color-editor.component.html',
  styleUrl: './display-color-editor.component.css'
})
export class DisplayColorEditorComponent {
  private readonly accountService = inject(AccountService);

  currentColor = input<string>();
  selectedColor = linkedSignal(() => this.currentColor());
  colorUpdated = output<void>();

  onSubmit(ev: Event): void {
    ev.preventDefault();

    const color = this.selectedColor();
    
    this.accountService.updateDisplayColor(color).subscribe({
      complete: () => this.colorUpdated.emit(),
    });
  }
}
