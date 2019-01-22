import { Injectable } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';

import { Observable, throwError } from 'rxjs';

import { MessageService } from './message.service';

/** Type of the handleError function returned by HttpErrorHandler.createHandleError */
export type HandleError = (operation?: string) => (error: HttpErrorResponse) => Observable<never>;

/** Handles HttpClient errors */
@Injectable({
  providedIn: 'root'
})
export class HttpErrorHandler {
  constructor(private messageService: MessageService) { }

  /** Create curried handleError function that already knows the service name */
  createHandleError = (serviceName = '') => (operation = 'operation') => this.handleError(serviceName, operation);

  /**
   * Returns a function that handles Http operation failures.
   * @param serviceName = name of the data service that attempted the operation
   * @param operation = name of the operation that failed
   */
  handleError(serviceName = '', operation = 'operation') {
    return (error: HttpErrorResponse): Observable<never> => {
      console.error(`${serviceName}: ${operation} failed`);
      console.error(error);

      if (error.error instanceof ErrorEvent) {
        this.messageService.add(`An error occurred: ${error.error.message}`);
      }
      else {
        if (error.status === 400) {
          let validationErrorDictionary = error.error;
          for (var fieldName in validationErrorDictionary) {
            if (validationErrorDictionary.hasOwnProperty(fieldName)) {
              this.messageService.add(validationErrorDictionary[fieldName]);
            }
          }
        }
        else {
          this.messageService.add('An error occurred; please try again later.');
        }
      }

      return throwError('An error occurred; please try again later.');
    };
  }
}
