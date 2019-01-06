import { Component, OnInit } from '@angular/core';

import { Band } from './band';
import { BandService } from './band.service';

@Component({
  selector: 'bands',
  templateUrl: './bands.component.html',
  providers: [ BandService ]
})
export class BandsComponent implements OnInit {
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
