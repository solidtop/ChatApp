import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';
import { RegisterFormComponent } from '../../components/register-form/register-form.component';

@Component({
  selector: 'app-register-page',
  imports: [RegisterFormComponent],
  templateUrl: './register-page.component.html',
  styleUrl: './register-page.component.css'
})
export class RegisterPageComponent {
  private readonly router = inject(Router);

  onRegistrationCompleted() {
    console.log('Registration completed!');
    this.router.navigateByUrl('/');
  }
}
