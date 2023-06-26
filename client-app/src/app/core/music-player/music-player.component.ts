import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Track } from 'src/app/shared/models/Track';
import { AccountService } from 'src/app/shared/service/account.service';
import { MusicService } from 'src/app/shared/service/music.service';

@Component({
  selector: 'app-music-player',
  templateUrl: './music-player.component.html',
  styleUrls: ['./music-player.component.css'],
})
export class MusicPlayerComponent implements OnInit {
  currentTrack?: Track;
  isCurrentTrackLiked = false;

  constructor(
    private musicService: MusicService,
    private accountService: AccountService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.musicService.currentTrack.subscribe({
      next: (track) => {
        if (track) {
          this.currentTrack = track;
          this.musicService.isTrackLiked(track.id).subscribe({
            next: (result) => (this.isCurrentTrackLiked = result),
          });
        } else {
          const result = localStorage.getItem('current_track');
          if (result) {
            this.currentTrack = JSON.parse(result) as Track;
            this.musicService.currentTrack.next(this.currentTrack);
            this.musicService.isTrackLiked(this.currentTrack.id).subscribe({
              next: (result) => (this.isCurrentTrackLiked = result),
            });
          }
        }
      },
    });
  }

  onTrackLiked() {
    if (!this.accountService.currentUser.value) {
      this.router.navigateByUrl('/account/login');
    } else {
      this.musicService.likeTrack().subscribe({
        next: () => {
          if (this.currentTrack) {
            this.musicService.isTrackLiked(this.currentTrack.id).subscribe({
              next: (result) => (this.isCurrentTrackLiked = result),
            });
          }
        },
      });
    }
  }

  onTrackUnliked() {
    if (!this.accountService.currentUser.value) {
      this.router.navigateByUrl('/account/login');
    } else {
      this.musicService.unlikeTrack().subscribe({
        next: () => {
          if (this.currentTrack) {
            this.musicService.isTrackLiked(this.currentTrack.id).subscribe({
              next: (result) => (this.isCurrentTrackLiked = result),
            });
          }
        },
      });
    }
  }
}
