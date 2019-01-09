import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { BandListComponent } from './band-list/band-list.component';
import { BandEditComponent } from './band-edit/band-edit.component';

import { BandsRoutingModule } from './bands-routing.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    BandsRoutingModule
  ],
  declarations: [
    BandListComponent,
    BandEditComponent
  ]
})
export class BandsModule { }
