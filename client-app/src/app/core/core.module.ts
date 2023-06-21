import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { RouterModule } from '@angular/router';
import { MusicPlayerComponent } from './music-player/music-player.component';

@NgModule({
  declarations: [NavBarComponent, MusicPlayerComponent],
  imports: [CommonModule, RouterModule],
  exports: [NavBarComponent, MusicPlayerComponent],
})
export class CoreModule {}
