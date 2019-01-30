import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { MusicianListComponent } from './musician-list/musician-list.component';

import { MusiciansRoutingModule } from './musicians-routing.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MusiciansRoutingModule
  ],
  declarations: [
    MusicianListComponent
  ]
})
export class MusiciansModule { }
