import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators, AbstractControl } from '@angular/forms';

import { Band } from '../band';
import { BandService } from '../band.service';
import { rangeValidator } from '../../shared/range-validator.directive';

@Component({
  selector: 'app-band-edit',
  templateUrl: './band-edit.component.html'
})
export class BandEditComponent implements OnInit {
  form: FormGroup;
  band: Band;
  isEditing: boolean;

  currentYear: number = new Date().getFullYear();

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private fb: FormBuilder,
    private bandService: BandService) {
    this.band = <Band>{};
  }

  ngOnInit() {
    this.createForm();
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
      .subscribe(band => {
        this.band = band;
        this.updateForm();
      });
  }

  createForm() {
    this.form = this.fb.group({
      name: ['', [
        Validators.required,
        Validators.maxLength(100)
        ]
      ],
      activeFromYear: ['', [
        Validators.required,
        Validators.pattern(/^\d*$/),
        rangeValidator(1900, this.currentYear)
        ]
      ],
      activeToYear: ['', [
        Validators.pattern(/^\d*$/),
        rangeValidator(1900, this.currentYear)
        ]
      ]
    });
  }

  updateForm() {
    this.form.setValue({
      name: this.band.name,
      activeFromYear: this.band.activeFromYear,
      activeToYear: this.band.activeToYear || ''
    });
  }

  onSubmit() {
    let band = this.createBandFromFormValues();
    if (this.isEditing) {
      this.bandService.updateBand(band)
        .subscribe(b => this.navigateToBandList());
    }
    else {
      this.bandService.addBand(band)
        .subscribe(b => this.navigateToBandList());
    }
  }

  createBandFromFormValues(): Band {
    let band = <Band>{};
    if (this.isEditing) {
      band.id = this.band.id;
    }
    band.name = this.form.value.name;
    band.activeFromYear = this.form.value.activeFromYear;
    band.activeToYear = this.form.value.activeToYear;
    return band;
  }

  onCancel() {
    this.navigateToBandList();
  }

  onDelete() {
    if (!confirm('Do you really want to delete this band?')) {
      return;
    }

    this.bandService.deleteBand(this.band.id)
      .subscribe(() => this.navigateToBandList());
  }

  navigateToBandList() {
    this.router.navigate(['/bands']);
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
