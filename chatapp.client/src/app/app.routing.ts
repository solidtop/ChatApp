import { Routes } from "@angular/router";
import { AppComponent } from "./app.component";
import { LoginPageComponent } from "./features/auth/pages/login-page/login-page.component";
import { RegisterPageComponent } from "./features/auth/pages/register-page/register-page.component";

export const routes: Routes = [
  { path: '', component: AppComponent },
  { path: 'register', component: RegisterPageComponent },
  { path: 'login', component: LoginPageComponent },
];
