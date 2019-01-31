import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { MusicianListComponent } from './musician-list/musician-list.component';
import { MusicianEditComponent } from './musician-edit/musician-edit.component';

const routes: Routes = [
  { path: 'musicians', component: MusicianListComponent },
  { path: 'musicians/create', component: MusicianEditComponent },
  { path: 'musicians/:id', component: MusicianEditComponent }
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
