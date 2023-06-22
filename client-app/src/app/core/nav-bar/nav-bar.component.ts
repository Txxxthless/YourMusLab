import { Component } from '@angular/core';
import { AccountService } from 'src/app/shared/service/account.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css'],
})
export class NavBarComponent {
  constructor(public accountService: AccountService) {}
}
