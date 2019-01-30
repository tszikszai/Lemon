import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { Musician } from '../musician';
import { MusicianService } from '../musician.service';
import { MessageService } from '../../message.service';

@Component({
  selector: 'app-musician-list',
  templateUrl: './musician-list.component.html'
})
export class MusicianListComponent implements OnInit {
  musicians: Musician[];

  constructor(
    private router: Router,
    private musicianService: MusicianService,
    private messageService: MessageService) { }

  ngOnInit() {
    this.messageService.clear();
    this.getMusicians();
  }

  getMusicians() {
    this.musicianService.getMusicians()
      .subscribe(
        musicians => this.musicians = musicians,
        () => this.musicians = []
      );
  }

  onCreate() {
    this.router.navigate(['/musicians/create']);
  }
}
