import { Component } from '@angular/core';
import { Genre } from '../shared/models/Genre';
import { Author } from '../shared/models/Author';
import { Track } from '../shared/models/Track';

@Component({
  selector: 'app-music-browser',
  templateUrl: './music-browser.component.html',
  styleUrls: ['./music-browser.component.css'],
})
export class MusicBrowserComponent {
  genres: Genre[] = [
    { id: 1, name: 'Progressive Rock' },
    { id: 2, name: 'Rock-n-Roll' },
  ];
  authors: Author[] = [
    { id: 1, name: 'King' },
    { id: 2, name: 'The Bottles' },
  ];
  tracks: Track[] = [
    {
      id: 1,
      name: 'Rock-n-Roll',
      genre: 'Progressive Rock',
      author: 'King',
      album: 'King II',
      filePath:
        'https://i.pinimg.com/564x/60/52/9f/60529fffa71e790ded04b0bd92c9ac8a.jpg',
    },
    {
      id: 2,
      name: 'Sorrow',
      genre: 'Rock-n-Roll',
      author: 'The Bottles',
      album: 'Bottles for Sale',
      filePath:
        'https://i.pinimg.com/564x/60/52/9f/60529fffa71e790ded04b0bd92c9ac8a.jpg',
    },
  ];
}
