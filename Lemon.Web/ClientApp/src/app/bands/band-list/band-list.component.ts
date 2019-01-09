import { Component, OnInit } from '@angular/core';

import { Band } from '../band';
import { BandService } from '../band.service';

@Component({
  selector: 'app-band-list',
  templateUrl: './band-list.component.html'
})
export class BandListComponent implements OnInit {
  bands: Band[];

  constructor(private bandService: BandService) { }

  ngOnInit() {
    this.getBands();
  }

  getBands() {
    this.bandService.getBands()
      .subscribe(bands => this.bands = bands);
  }
}
