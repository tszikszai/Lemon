import { Injectable } from '@angular/core';

import * as moment from 'moment';

const dateFormat = 'YYYY-MM-DD';

@Injectable({
  providedIn: 'root'
})
export class DateTimeService {
  get now(): moment.Moment {
    return moment();
  }

  get nowString(): string {
    return this.formatDateForDateInput(this.now);
  }

  get today(): moment.Moment {
    return moment().startOf('day');
  }

  get todayString(): string {
    return this.formatDateForDateInput(this.today);
  }

  get minimumDate(): moment.Moment {
    return moment({ year: 1900, month: 0, day: 1 });
  }

  get minimumDateString(): string {
    return this.formatDateForDateInput(this.minimumDate);
  }

  formatDate(value: any, format?: string): string {
    return moment(value).format(format);
  }

  formatDateForDateInput(value: any): string {
    if (!value) {
      return '';
    }
    return this.formatDate(value, dateFormat);
  }
}
