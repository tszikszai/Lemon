import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Band } from './band';
import { BandService } from './band.service';

@Component({
  selector: 'band-edit',
  templateUrl: './band-edit.component.html',
  providers: [ BandService ]
})
export class BandEditComponent implements OnInit {
  band: Band;
  isEditing: boolean;

  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private bandService: BandService) {
    this.band = <Band>{};
  }

  ngOnInit() {
    let id: number = +this.activatedRoute.snapshot.paramMap.get('id');
    if (id) {
      this.isEditing = true;
      this.getBand(id);
    }
    else {
      this.isEditing = false;
    }
  }

  getBand(id: number) {
    this.bandService.getBand(id)
      .subscribe(band => this.band = band);
  }
}
