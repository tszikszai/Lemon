import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export function dateRangeValidator(minimum: Date, maximum: Date): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    let isOutOfRange: boolean = control.value != null && control.value !== '' && (new Date(control.value) < minimum || new Date(control.value) > maximum);
    return isOutOfRange ? { 'dateRange': { value: control.value } } : null;
  }
}
