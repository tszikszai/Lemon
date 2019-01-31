import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { MusicianListComponent } from './musician-list/musician-list.component';
import { MusicianEditComponent } from './musician-edit/musician-edit.component';

import { MusiciansRoutingModule } from './musicians-routing.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MusiciansRoutingModule
  ],
  declarations: [
    MusicianListComponent,
    MusicianEditComponent
  ]
})
export class MusiciansModule { }
