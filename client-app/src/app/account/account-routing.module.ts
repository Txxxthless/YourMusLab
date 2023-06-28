import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { AccountDetailComponent } from './account-detail/account-detail.component';
import { PasswordResetComponent } from './password-reset/password-reset.component';
import { PasswordResetSuccessComponent } from './password-reset-success/password-reset-success.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  {path: 'forgot-password', component: PasswordResetComponent},
  {path: 'forgot-password-success', component: PasswordResetSuccessComponent},
  { path: '', component: AccountDetailComponent },
];

@NgModule({
  declarations: [],
  imports: [CommonModule, RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AccountRoutingModule {}
