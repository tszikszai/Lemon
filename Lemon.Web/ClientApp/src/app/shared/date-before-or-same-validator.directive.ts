import { AbstractControl, FormGroup, ValidationErrors, ValidatorFn } from '@angular/forms';

import * as moment from 'moment';

export function dateBeforeOrSameValidator(nameFirst: string, nameSecond: string): ValidatorFn {
  return (formGroup: FormGroup): ValidationErrors | null => {
    let controlFirst: AbstractControl = formGroup.get(nameFirst);
    let controlSecond: AbstractControl = formGroup.get(nameSecond);

    return controlFirst && controlSecond && !moment(controlFirst.value || '0001-01-01').isSameOrBefore(controlSecond.value || '9999-12-31') ? { 'dateBeforeOrSame': true } : null;
  };
}
