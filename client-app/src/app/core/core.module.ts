import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { RouterModule } from '@angular/router';
import { MusicPlayerComponent } from './music-player/music-player.component';
import { LoaderComponent } from './loader/loader.component';

@NgModule({
  declarations: [NavBarComponent, MusicPlayerComponent, LoaderComponent],
  imports: [CommonModule, RouterModule],
  exports: [NavBarComponent, MusicPlayerComponent, LoaderComponent],
})
export class CoreModule {}
