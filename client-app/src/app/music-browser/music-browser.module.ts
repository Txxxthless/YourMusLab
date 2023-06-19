import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MusicBrowserComponent } from './music-browser.component';
import { MusicBrowserRoutingModule } from './music-browser-routing.module';
import { TrackItemComponent } from './track-item/track-item.component';

@NgModule({
  declarations: [MusicBrowserComponent, TrackItemComponent],
  imports: [CommonModule, MusicBrowserRoutingModule],
})
export class MusicBrowserModule {}
