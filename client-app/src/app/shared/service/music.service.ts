import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Track } from '../models/Track';
import { Author } from '../models/Author';
import { Genre } from '../models/Genre';
import { TrackParams } from '../models/TrackParams';
import { BehaviorSubject } from 'rxjs';
import { LoadingService } from './loading.service';

@Injectable({
  providedIn: 'root',
})
export class MusicService {
  baseUrl = 'https://localhost:5002/api/';

  currentTrack = new BehaviorSubject<Track | null>(null);

  constructor(
    private http: HttpClient,
    private loadingService: LoadingService
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

    const observableToReturn = this.http.get<Track[]>(
      this.baseUrl + 'music/tracks',
      {
        params: params,
      }
    );
    
    this.loadingService.endLoading();
    return observableToReturn;
  }

  getAuthors() {
    this.loadingService.beginLoading();
    const observableToReturn = this.http.get<Author[]>(
      this.baseUrl + 'music/authors'
    );
    this.loadingService.endLoading();
    return observableToReturn;
  }

  getGenres() {
    this.loadingService.beginLoading();
    const observableToReturn = this.http.get<Genre[]>(
      this.baseUrl + 'music/genres'
    );
    this.loadingService.endLoading();
    return observableToReturn;
  }

  getCurrentTrack(): Track | undefined {
    const currentTrack = localStorage.getItem('current_track');
    if (currentTrack) {
      return JSON.parse(currentTrack) as Track;
    } else return undefined;
  }
}
