import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/shared/service/account.service';

@Component({
  selector: 'app-password-reset',
  templateUrl: './password-reset.component.html',
  styleUrls: ['./password-reset.component.css'],
})
export class PasswordResetComponent {
  passwordExp =
    "(?=^.{6,10}$)(?=.*d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*s).*$";

  passwordResetForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    newPassword: new FormControl('', [
      Validators.required,
      Validators.pattern(this.passwordExp),
    ]),
  });

  constructor(private accountService: AccountService, private router: Router) {}

  onSubmit() {
    if (
      this.passwordResetForm.value.email &&
      this.passwordResetForm.value.newPassword
    ) {
      this.accountService
        .forgotPassword(
          this.passwordResetForm.value.email,
          this.passwordResetForm.value.newPassword
        )
        .subscribe({
          next: () =>
            this.router.navigateByUrl('/account/forgot-password-success'),
        });
    }
  }

  get email() {
    return this.passwordResetForm.get('email');
  }

  get newPassword() {
    return this.passwordResetForm.get('newPassword');
  }
}
