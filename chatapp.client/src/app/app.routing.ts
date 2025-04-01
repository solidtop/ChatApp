import { Routes } from "@angular/router";
import { AccountPageComponent } from "./features/account/pages/account-page/account-page.component";
import { authGuard } from "./features/auth/guards/auth.guard";
import { LoginPageComponent } from "./features/auth/pages/login-page/login-page.component";
import { RegisterPageComponent } from "./features/auth/pages/register-page/register-page.component";
import { HomePageComponent } from "./features/home/home-page/home-page.component";

export const routes: Routes = [
  { path: '', component: HomePageComponent },
  { path: 'register', component: RegisterPageComponent },
  { path: 'login', component: LoginPageComponent },
  { path: 'account', component: AccountPageComponent, canActivate: [authGuard] },
];
