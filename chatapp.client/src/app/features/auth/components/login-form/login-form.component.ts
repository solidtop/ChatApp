import { Component, inject, output } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Subject } from '@microsoft/signalr';
import { FormComponent } from '../../../../shared/components/form/form.component';
import { PrimaryButtonComponent } from '../../../../shared/components/primary-button/primary-button.component';
import { TextFieldComponent } from '../../../../shared/components/text-field/text-field.component';
import { LoginRequest } from '../../interfaces/login-request.interface';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login-form',
  imports: [ReactiveFormsModule, FormComponent, TextFieldComponent, PrimaryButtonComponent],
  templateUrl: './login-form.component.html',
  styleUrl: './login-form.component.css'
})
export class LoginFormComponent {
  private readonly formBuilder = inject(FormBuilder);
  private readonly authService = inject(AuthService);
  destroy$ = new Subject<void>();
  loginFailed = false;
  loginCompleted = output<void>();

  readonly form: FormGroup = this.formBuilder.group({
    email: ['', Validators.email],
    password: ['', Validators.required],
  });

  onSubmit(ev: Event) {
    ev.preventDefault();
    this.loginFailed = false;

    const request = this.form.value as LoginRequest;

    this.authService.login(request)
      .subscribe({
        error: () => this.loginFailed = true,
        complete: () => this.loginCompleted.emit(),
      });
  }
}
