import { AsyncPipe } from '@angular/common';
import { Component, inject, output } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AccountStateService } from '../../../account/services/account-state.service';

@Component({
  selector: 'app-chat-input',
  imports: [ReactiveFormsModule, AsyncPipe],
  templateUrl: './chat-input.component.html',
  styleUrl: './chat-input.component.css'
})
export class ChatInputComponent {
  private readonly formBuilder = inject(FormBuilder);
  public readonly accountStateService = inject(AccountStateService);
  messageSubmitted = output<string>();
  
  readonly form: FormGroup = this.formBuilder.group({
    text: ['', Validators.required],
  });

  onSubmit(ev: Event): void {
    ev.preventDefault();

    const text = this.form.value['text'] as string;
    this.messageSubmitted.emit(text);

    this.form.patchValue({ text: ''});
  }
}
