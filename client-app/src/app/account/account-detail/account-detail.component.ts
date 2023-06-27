import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Track } from 'src/app/shared/models/Track';
import { User } from 'src/app/shared/models/user';
import { AccountService } from 'src/app/shared/service/account.service';
import { MusicService } from 'src/app/shared/service/music.service';

@Component({
  selector: 'app-account-detail',
  templateUrl: './account-detail.component.html',
  styleUrls: ['./account-detail.component.css'],
})
export class AccountDetailComponent implements OnInit {
  currentUser?: User;
  userLikedTracks?: Track[];

  constructor(
    private accountService: AccountService,
    private router: Router,
    private musicService: MusicService
  ) {}

  ngOnInit(): void {
    if (this.accountService.currentUser.value) {
      this.currentUser = this.accountService.currentUser.value;
      this.musicService.getLikedTracksForUser().subscribe({
        next: (tracks) => (this.userLikedTracks = tracks),
      });
    } else {
      this.router.navigateByUrl('/account/login');
    }
  }

  logout() {
    this.accountService.logout();
  }
}
