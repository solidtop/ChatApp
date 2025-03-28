import { HttpErrorResponse } from '@angular/common/http';
import { Component, inject, output } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ButtonComponent } from '../../../../shared/components/button/button.component';
import { FormComponent } from '../../../../shared/components/form/form.component';
import { TextFieldComponent } from '../../../../shared/components/text-field/text-field.component';
import { IdentityError } from '../../interfaces/identity-error.interface';
import { RegisterRequest } from '../../interfaces/register-request.interface';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-register-form',
  imports: [ReactiveFormsModule, FormComponent, TextFieldComponent, ButtonComponent],
  templateUrl: './register-form.component.html',
  styleUrl: './register-form.component.css'
})
export class RegisterFormComponent {
  private readonly formBuilder = inject(FormBuilder);
  private readonly authService = inject(AuthService);
  errors: IdentityError[] = [];
  registrationCompleted = output<void>();

  readonly form: FormGroup = this.formBuilder.group({
    username: ['', Validators.required],
    email: ['', Validators.email],
    password: ['', [Validators.required, Validators.minLength(8)]],
  });

  onSubmit(ev: Event) {
    ev.preventDefault();
    this.errors = [];

    const request = this.form.value as RegisterRequest;

    this.authService.register(request)
      .subscribe({
        error: (response: HttpErrorResponse) => this.errors = response.error,
        complete: () => this.registrationCompleted.emit(),
      });
  }

  get username() {
    return this.form.get('username');
  }

  get email() {
    return this.form.get('email');
  }

  get password() {
    return this.form.get('password');
  }
}
