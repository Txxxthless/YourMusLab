import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { AccountRoutingModule } from './account-routing.module';
import { ReactiveFormsModule } from '@angular/forms';
import { AccountDetailComponent } from './account-detail/account-detail.component';
import { MusicBrowserModule } from '../music-browser/music-browser.module';

@NgModule({
  declarations: [LoginComponent, RegisterComponent, AccountDetailComponent],
  imports: [CommonModule, AccountRoutingModule, ReactiveFormsModule, MusicBrowserModule],
})
export class AccountModule {}
