import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators, AbstractControl } from '@angular/forms';

import { Musician } from '../musician';
import { MusicianService } from '../musician.service';
import { MessageService } from '../../message.service';
import { DateTimeService } from '../../shared/date-time.service';
import { dateRangeValidator } from '../../shared/date-range-validator.directive';
import { dateBeforeOrSameValidator } from '../../shared/date-before-or-same-validator.directive';

@Component({
  selector: 'app-musician-edit',
  templateUrl: './musician-edit.component.html'
})
export class MusicianEditComponent implements OnInit {
  form: FormGroup;
  musician: Musician;
  isEditing: boolean;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private fb: FormBuilder,
    private musicianService: MusicianService,
    private messageService: MessageService,
    private dateTimeService: DateTimeService) {
    this.musician = <Musician>{};
  }

  ngOnInit() {
    this.messageService.clear();
    this.createForm();
    let id: number = +this.route.snapshot.paramMap.get('id');
    if (id) {
      this.isEditing = true;
      this.getMusician(id);
    }
    else {
      this.isEditing = false;
    }
  }

  getMusician(id: number) {
    this.musicianService.getMusician(id)
      .subscribe(musician => {
        this.musician = musician;
        this.updateForm();
      });
  }

  createForm() {
    this.form = this.fb.group({
      firstName: ['', [
        Validators.required,
        Validators.maxLength(50)
        ]
      ],
      lastName: ['', [
        Validators.required,
        Validators.maxLength(50)
        ]
      ],
      dateOfBirth: ['', [
        Validators.required,
        dateRangeValidator(this.dateTimeService.minimumDate, this.dateTimeService.today)
        ]
      ],
      dateOfDeath: ['', dateRangeValidator(this.dateTimeService.minimumDate, this.dateTimeService.today)]
    }, { validators: dateBeforeOrSameValidator('dateOfBirth', 'dateOfDeath') });
  }

  updateForm() {
    this.form.setValue({
      firstName: this.musician.firstName,
      lastName: this.musician.lastName,
      dateOfBirth: this.dateTimeService.formatDateForDateInput(this.musician.dateOfBirth),
      dateOfDeath: this.dateTimeService.formatDateForDateInput(this.musician.dateOfDeath)
    });
  }

  onSubmit() {
    this.messageService.clear();
    let musician = this.createMusicianFromFormValues();
    if (this.isEditing) {
      this.musicianService.updateMusician(musician)
        .subscribe(() => this.navigateToMusicianList());
    }
    else {
      this.musicianService.addMusician(musician)
        .subscribe(() => this.navigateToMusicianList());
    }
  }

  createMusicianFromFormValues(): Musician {
    let musician = <Musician>{};
    if (this.isEditing) {
      musician.id = this.musician.id;
    }
    musician.firstName = this.form.value.firstName;
    musician.lastName = this.form.value.lastName;
    musician.dateOfBirth = this.form.value.dateOfBirth;
    musician.dateOfDeath = this.form.value.dateOfDeath;
    return musician;
  }

  onCancel() {
    this.navigateToMusicianList();
  }

  onDelete() {
    if (!confirm('Do you really want to delete this musician?')) {
      return;
    }

    this.messageService.clear();

    this.musicianService.deleteMusician(this.musician.id)
      .subscribe(() => this.navigateToMusicianList());
  }

  navigateToMusicianList() {
    this.router.navigate(['/musicians']);
  }

  getFormControl(name: string): AbstractControl {
    return this.form.get(name);
  }

  hasError(name: string): boolean {
    let control: AbstractControl = this.getFormControl(name);
    return control && control.invalid && (control.dirty || control.touched);
  }

  getError(name: string, errorCode: string): any {
    let control: AbstractControl = this.getFormControl(name);
    return control.getError(errorCode);
  }
}
