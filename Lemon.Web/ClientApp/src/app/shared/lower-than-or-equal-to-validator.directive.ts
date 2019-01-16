import { AbstractControl, FormGroup, ValidationErrors, ValidatorFn } from '@angular/forms';

export function lowerThanOrEqualToValidator(nameFirst: string, nameSecond: string): ValidatorFn {
  return (formGroup: FormGroup): ValidationErrors | null => {
    let controlFirst: AbstractControl = formGroup.get(nameFirst);
    let controlSecond: AbstractControl = formGroup.get(nameSecond);

    return controlFirst && controlSecond && (controlFirst.value || Number.MIN_VALUE) > (controlSecond.value || Number.MAX_VALUE) ? { 'lowerThanOrEqualTo': true } : null;
  };
}
