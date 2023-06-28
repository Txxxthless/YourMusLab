import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/shared/service/account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  loginForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required]),
  });

  constructor(private accountService: AccountService, private router: Router) {}

  onSubmit() {
    if (this.loginForm.value.email && this.loginForm.value.password) {
      this.accountService
        .login(this.loginForm.value.email, this.loginForm.value.password)
        .subscribe({
          next: (user) => this.router.navigateByUrl('/'),
        });
    }
  }

  get email() {
    return this.loginForm.get('email');
  }

  get password() {
    return this.loginForm.get('password');
  }
}
