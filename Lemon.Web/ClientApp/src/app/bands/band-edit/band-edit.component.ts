import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Band } from '../band';
import { BandService } from '../band.service';

@Component({
  selector: 'app-band-edit',
  templateUrl: './band-edit.component.html'
})
export class BandEditComponent implements OnInit {
  band: Band;
  isEditing: boolean;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private bandService: BandService) {
    this.band = <Band>{};
  }

  ngOnInit() {
    let id: number = +this.route.snapshot.paramMap.get('id');
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
