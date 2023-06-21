import { Component, OnInit } from '@angular/core';
import { Track } from 'src/app/shared/models/Track';
import { MusicService } from 'src/app/shared/service/music.service';

@Component({
  selector: 'app-music-player',
  templateUrl: './music-player.component.html',
  styleUrls: ['./music-player.component.css'],
})
export class MusicPlayerComponent implements OnInit {
  currentTrack?: Track;

  constructor(private musicService: MusicService) {}

  ngOnInit(): void {
    this.musicService.currentTrack.subscribe({
      next: (track) => {
        if (track) {
          this.currentTrack = track;
        } else {
          const result = localStorage.getItem('current_track');
          if (result) {
            this.currentTrack = JSON.parse(result) as Track;
          }
        }
      },
    });
  }
}
