import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Track } from '../models/Track';
import { Author } from '../models/Author';
import { Genre } from '../models/Genre';
import { TrackParams } from '../models/TrackParams';
import { BehaviorSubject, Observable, map } from 'rxjs';
import { LoadingService } from './loading.service';
import { AccountService } from './account.service';

@Injectable({
  providedIn: 'root',
})
export class MusicService {
  baseUrl = 'https://localhost:5002/api/';

  currentTrack = new BehaviorSubject<Track | null>(null);
  isCurrentTrackLiked = new BehaviorSubject<boolean>(false);

  constructor(
    private http: HttpClient,
    private loadingService: LoadingService,
    private accountService: AccountService
  ) {}

  getMusic(trackParams: TrackParams) {
    this.loadingService.beginLoading();

    let params = new HttpParams();
    if (trackParams.id) {
      params = params.append('id', trackParams.id);
    }
    if (trackParams.search) {
      params = params.append('search', trackParams.search);
    }
    if (trackParams.author) {
      params = params.append('author', trackParams.author);
    }
    if (trackParams.genre) {
      params = params.append('genre', trackParams.genre);
    }

    return this.http
      .get<Track[]>(this.baseUrl + 'music/tracks', {
        params: params,
      })
      .pipe(
        map((track) => {
          this.loadingService.endLoading();
          return track;
        })
      );
  }

  getAuthors() {
    this.loadingService.beginLoading();
    return this.http.get<Author[]>(this.baseUrl + 'music/authors').pipe(
      map((author) => {
        this.loadingService.endLoading();
        return author;
      })
    );
  }

  getGenres() {
    this.loadingService.beginLoading();
    return this.http.get<Genre[]>(this.baseUrl + 'music/genres').pipe(
      map((genre) => {
        this.loadingService.endLoading();
        return genre;
      })
    );
  }

  getCurrentTrack(): Track | undefined {
    const currentTrack = localStorage.getItem('current_track');
    if (currentTrack) {
      return JSON.parse(currentTrack) as Track;
    } else return undefined;
  }

  likeTrack() {
    if (this.currentTrack.value && this.accountService.currentUser.value) {
      const trackId = this.currentTrack.value.id;

      return this.http.post(this.baseUrl + 'music/liketrack', {
        trackId,
      });
    }
    return new Observable();
  }

  unlikeTrack() {
    if (this.currentTrack.value && this.accountService.currentUser.value) {
      const trackId = this.currentTrack.value.id;

      return this.http.post(this.baseUrl + 'music/unliketrack', {
        trackId,
      });
    }
    return new Observable();
  }

  isTrackLiked(trackId: number) {
    return this.http.get<boolean>(
      this.baseUrl + `music/isliked?trackId=${trackId}`
    );
  }
}
