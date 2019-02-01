import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

import * as moment from 'moment';

export function dateRangeValidator(minimum: moment.Moment, maximum: moment.Moment): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    let isOutOfRange: boolean = control.value != null && control.value !== '' && (!moment(control.value).isBetween(minimum, maximum, null, '[]'));
    return isOutOfRange ? { 'dateRange': { value: control.value } } : null;
  }
}
