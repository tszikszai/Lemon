import { Inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { Band } from './band';
import { HttpErrorHandler, HandleError } from '../http-error-handler.service';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
};

@Injectable({
  providedIn: 'root'
})
export class BandService {
  private handleError: HandleError;

  get bandsUrl(): string {
    return this.baseUrl + 'api/bands';
  }

  constructor(
    @Inject('BASE_URL') private baseUrl: string,
    private http: HttpClient,
    httpErrorHandler: HttpErrorHandler) {
    this.handleError = httpErrorHandler.createHandleError('BandService');
  }

  getBands(): Observable<Band[]> {
    return this.http.get<Band[]>(this.bandsUrl)
      .pipe(
        catchError(this.handleError('getBands'))
      );
  }

  getBand(id: number): Observable<Band> {
    const url = `${this.bandsUrl}/${id}`;
    return this.http.get<Band>(url)
      .pipe(
        catchError(this.handleError('getBand'))
      );
  }

  addBand(band: Band): Observable<Band> {
    return this.http.post<Band>(this.bandsUrl, band, httpOptions)
      .pipe(
        catchError(this.handleError('addBand'))
      );
  }

  updateBand(band: Band): Observable<{}> {
    const url = `${this.bandsUrl}/${band.id}`;
    return this.http.put<Band>(url, band, httpOptions)
      .pipe(
        catchError(this.handleError('updateBand'))
      );
  }

  deleteBand(id: number): Observable<{}> {
    const url = `${this.bandsUrl}/${id}`;
    return this.http.delete(url)
      .pipe(
        catchError(this.handleError('deleteBand'))
      );
  }
}
