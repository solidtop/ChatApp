import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';
import { LoginFormComponent } from '../../components/login-form/login-form.component';

@Component({
  selector: 'app-login-page',
  imports: [LoginFormComponent],
  templateUrl: './login-page.component.html',
  styleUrl: './login-page.component.css'
})
export class LoginPageComponent {
  private readonly router = inject(Router);

  onLoginCompleted() {
    console.log('Login completed!');
    this.router.navigateByUrl('/');
  }
}
