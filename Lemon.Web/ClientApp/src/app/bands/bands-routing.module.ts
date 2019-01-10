import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { BandListComponent } from './band-list/band-list.component';
import { BandEditComponent } from './band-edit/band-edit.component';

const routes: Routes = [
  { path: 'bands', component: BandListComponent },
  { path: 'bands/create', component: BandEditComponent },
  { path: 'bands/:id', component: BandEditComponent }
];

@NgModule({
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class BandsRoutingModule { }
