import { Inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { Musician } from './musician';
import { HttpErrorHandler, HandleError } from '../http-error-handler.service';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
};

@Injectable({
  providedIn: 'root'
})
export class MusicianService {
  private handleError: HandleError;

  get musiciansUrl(): string {
    return this.baseUrl + 'api/musicians';
  }

  constructor(
    @Inject('BASE_URL') private baseUrl: string,
    private http: HttpClient,
    httpErrorHandler: HttpErrorHandler) {
    this.handleError = httpErrorHandler.createHandleError('MusicianService');
  }

  getMusicians(): Observable<Musician[]> {
    return this.http.get<Musician[]>(this.musiciansUrl)
      .pipe(
        catchError<Musician[], Musician[]>(this.handleError('getMusicians'))
      );
  }

  getMusician(id: number): Observable<Musician> {
    const url = `${this.musiciansUrl}/${id}`;
    return this.http.get<Musician>(url)
      .pipe(
        catchError<Musician, Musician>(this.handleError('getMusician'))
      );
  }

  addMusician(musician: Musician): Observable<Musician> {
    return this.http.post<Musician>(this.musiciansUrl, musician, httpOptions)
      .pipe(
        catchError<Musician, Musician>(this.handleError('addMusician'))
      );
  }

  updateMusician(musician: Musician): Observable<{}> {
    const url = `${this.musiciansUrl}/${musician.id}`;
    return this.http.put<Musician>(url, musician, httpOptions)
      .pipe(
        catchError(this.handleError('updateMusician'))
      );
  }

  deleteMusician(id: number): Observable<{}> {
    const url = `${this.musiciansUrl}/${id}`;
    return this.http.delete(url)
      .pipe(
        catchError(this.handleError('deleteMusician'))
      );
  }
}
