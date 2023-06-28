import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';
import { Router } from '@angular/router';
import { User } from '../models/user';
import { LoadingService } from './loading.service';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  baseUrl = 'https://localhost:5002/api/';

  currentUser = new BehaviorSubject<User | null>(null);

  constructor(private http: HttpClient, private router: Router) {}

  login(email: string, password: string) {
    return this.http
      .post<User>(this.baseUrl + 'account/login', {
        email,
        password,
      })
      .pipe(
        map((user) => {
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUser.next(user);
        })
      );
  }

  register(email: string, password: string, username: string) {
    return this.http
      .post<User>(this.baseUrl + 'account/register', {
        email,
        password,
        username,
      })
      .pipe(
        map((user) => {
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUser.next(user);
        })
      );
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUser.next(null);
    this.router.navigateByUrl('/');
  }

  forgotPassword(email?: string, password?: string) {
    return this.http.post(this.baseUrl + 'account/forgotpassword', {
      email,
      password,
    });
  }
}
