import { Component, Input } from '@angular/core';
import { Track } from 'src/app/shared/models/Track';

@Component({
  selector: 'app-track-item',
  templateUrl: './track-item.component.html',
  styleUrls: ['./track-item.component.css']
})
export class TrackItemComponent {
  @Input() track?: Track;
}
