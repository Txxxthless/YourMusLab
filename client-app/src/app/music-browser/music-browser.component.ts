import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Genre } from '../shared/models/Genre';
import { Author } from '../shared/models/Author';
import { Track } from '../shared/models/Track';
import { MusicService } from '../shared/service/music.service';
import { TrackParams } from '../shared/models/TrackParams';

@Component({
  selector: 'app-music-browser',
  templateUrl: './music-browser.component.html',
  styleUrls: ['./music-browser.component.css'],
})
export class MusicBrowserComponent implements OnInit {
  authors?: Author[];
  genres?: Genre[];
  tracks?: Track[];

  searchParams: TrackParams = new TrackParams();

  @ViewChild('search') search?: ElementRef;

  constructor(private musicService: MusicService) {}

  ngOnInit(): void {
    this.getAuthors();
    this.getGenres();
    this.getTracks();
  }

  onSearch() {
    this.searchParams.search = this.search?.nativeElement.value;
  }

  onGenreSelected(event: any) {
    this.searchParams.genre = event.target?.value;
    console.log(this.searchParams);
  }

  onAuthorSelected(event: any) {
    this.searchParams.author = event.target?.value;
    console.log(this.searchParams);
  }

  getTracks() {
    if (this.searchParams) {
      this.musicService
        .getMusic(this.searchParams)
        .subscribe({ next: (tracks) => (this.tracks = tracks) });
      if (this.search) {
        this.search.nativeElement.value = '';
        this.searchParams.search = '';
      }
    }
  }

  getAuthors() {
    this.musicService
      .getAuthors()
      .subscribe({ next: (authors) => (this.authors = authors) });
  }

  getGenres() {
    this.musicService
      .getGenres()
      .subscribe({ next: (genres) => (this.genres = genres) });
  }
}
