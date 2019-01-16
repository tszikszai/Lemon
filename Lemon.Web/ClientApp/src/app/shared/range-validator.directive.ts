import { AbstractControl, ValidatorFn } from '@angular/forms';

export function rangeValidator(minimum: number, maximum: number): ValidatorFn {
  return (control: AbstractControl): { [key: string]: any } | null => {
    let isOutOfRange: boolean = control.value != null && control.value !== '' && (control.value < minimum || control.value > maximum);
    return isOutOfRange ? { 'range': { value: control.value } } : null;
  };
}
