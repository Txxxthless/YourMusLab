import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/shared/models/user';
import { AccountService } from 'src/app/shared/service/account.service';

@Component({
  selector: 'app-account-detail',
  templateUrl: './account-detail.component.html',
  styleUrls: ['./account-detail.component.css'],
})
export class AccountDetailComponent implements OnInit {
  currentUser?: User;

  constructor(private accountService: AccountService, private router: Router) {}

  ngOnInit(): void {
    if (this.accountService.currentUser.value) {
      this.currentUser = this.accountService.currentUser.value;
    } else {
      this.router.navigateByUrl('/account/login');
    }
  }

  logout() {
    this.accountService.logout();
  }
}
