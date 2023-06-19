import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { MusicBrowserComponent } from './music-browser.component';

const routes: Routes = [{ path: '', component: MusicBrowserComponent }];

@NgModule({
  declarations: [],
  imports: [CommonModule, RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class MusicBrowserRoutingModule {}
