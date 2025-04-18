import { AsyncPipe } from '@angular/common';
import { Component, inject, output } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AccountStateService } from '../../../account/services/account-state.service';
import { ChatCommandService } from '../../services/chat-command.service';

@Component({
  selector: 'app-chat-input',
  imports: [ReactiveFormsModule, AsyncPipe],
  templateUrl: './chat-input.component.html',
  styleUrl: './chat-input.component.css'
})
export class ChatInputComponent {
  private readonly formBuilder = inject(FormBuilder);
  readonly accountStateService = inject(AccountStateService);
  readonly chatCommandService = inject(ChatCommandService);
  messageSubmitted = output<string>();
  commandSubmitted = output<string>();
  
  readonly form: FormGroup = this.formBuilder.group({
    text: ['', Validators.required],
  });

  onChange(): void {
    console.log('change');
  }

  onSubmit(ev: Event): void {
    ev.preventDefault();

    const text = this.form.value['text'] as string;

    if (this.chatCommandService.isCommand(text)) {
      this.commandSubmitted.emit(text);
    } else {
      this.messageSubmitted.emit(text);
    }

    this.form.patchValue({ text: ''});
  }
}
