import { Component, inject, output } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { take } from 'rxjs';
import { LoginRequest } from '../../interfaces/login-request.interface';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login-form',
  imports: [ReactiveFormsModule],
  templateUrl: './login-form.component.html',
  styleUrl: './login-form.component.css'
})
export class LoginFormComponent {
  private readonly formBuilder = inject(FormBuilder);
  private readonly authService = inject(AuthService);
  loginFailed = false;
  loginCompleted = output<void>();

  readonly form: FormGroup = this.formBuilder.group({
    email: ['', Validators.email],
    password: ['', Validators.required],
  });

  handleLogin(ev: Event) {
    ev.preventDefault();
    this.loginFailed = false;

    const request = this.form.value as LoginRequest;

    this.authService.login(request)
      .pipe(take(1))
      .subscribe({
        error: () => this.loginFailed = true,
        complete: () => this.loginCompleted.emit(),
      });
  }
}
