import { Component, OnInit } from '@angular/core';
import { AccountService } from './shared/service/account.service';
import { User } from './shared/models/user';
import { LoadingService } from './shared/service/loading.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  constructor(
    private accountService: AccountService,
    public loadingService: LoadingService
  ) {}

  ngOnInit(): void {
    const userFromLocalStorage = localStorage.getItem('user');
    if (userFromLocalStorage) {
      const user = JSON.parse(userFromLocalStorage) as User;
      this.accountService.currentUser.next(user);
    }
  }
}
