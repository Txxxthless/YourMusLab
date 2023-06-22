import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/shared/service/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent {
  registerForm = new FormGroup({
    username: new FormControl('', [Validators.required]),
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required]),
  });

  constructor(private accountService: AccountService, private router: Router) {}

  onSubmit() {
    if (
      this.registerForm.value.email &&
      this.registerForm.value.password &&
      this.registerForm.value.username
    ) {
      this.accountService
        .register(
          this.registerForm.value.email,
          this.registerForm.value.password,
          this.registerForm.value.username
        )
        .subscribe({
          next: (user) => this.router.navigateByUrl('/'),
        });
    }
  }
}
