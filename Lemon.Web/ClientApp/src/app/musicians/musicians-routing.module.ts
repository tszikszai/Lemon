import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { MusicianListComponent } from './musician-list/musician-list.component';

const routes: Routes = [
  { path: 'musicians', component: MusicianListComponent }
];

@NgModule({
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class MusiciansRoutingModule { }
