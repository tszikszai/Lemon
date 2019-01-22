import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { Band } from '../band';
import { BandService } from '../band.service';
import { MessageService } from '../../message.service';

@Component({
  selector: 'app-band-list',
  templateUrl: './band-list.component.html'
})
export class BandListComponent implements OnInit {
  bands: Band[];

  constructor(
    private router: Router,
    private bandService: BandService,
    private messageService: MessageService) { }

  ngOnInit() {
    this.messageService.clear();
    this.getBands();
  }

  getBands() {
    this.bandService.getBands()
      .subscribe(
        bands => this.bands = bands,
        () => this.bands = []
      );
  }

  onCreate() {
    this.router.navigate(['/bands/create']);
  }
}
