import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams, HttpRequest } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

export class ModelHttpOptions {
  headers?: HttpHeaders | {
    [header: string]: string | string[];
  };
  observe?: 'body';
  params?: HttpParams | {
    [param: string]: string | string[];
  };
  reportProgress?: boolean;
  responseType?: 'json';
  withCredentials?: boolean;
}

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(
    private httpClient: HttpClient,
    @Inject('BASE_APP_URL') public urlApi: string) { }

  public get<T>(path: string, params?: HttpParams, headers?: HttpHeaders): Observable<T> {
    const httpOptions = this.getBasicHttpOptions();
    if (params)
      httpOptions.params = params;
    if (headers)
      httpOptions.headers = headers;

    return this.httpClient.get<T>(this.urlApi + path, httpOptions)
      .pipe(catchError(this.formatErrors));
  }

  public post<T>(path: string, body?: object, params?: HttpParams, headers?: HttpHeaders): Observable<T> {
    const httpOptions = this.getBasicHttpOptions();
    if (params)
      httpOptions.params = params;
    if (headers)
      httpOptions.headers = headers;

    return this.httpClient.post<T>(this.urlApi + path, JSON.stringify(body), httpOptions)
      .pipe(catchError(this.formatErrors));
  }

  public delete<T>(path: string, params?: HttpParams) {
    const httpOptions = this.getBasicHttpOptions();
    if (params)
      httpOptions.params = params;

    return this.httpClient.delete<T>(this.urlApi + path, httpOptions)
      .pipe(catchError(this.formatErrors));
  }

  public put<T>(path: string, body: object, params?: HttpParams) {
    const httpOptions = this.getBasicHttpOptions();
    if (params)
      httpOptions.params = params;

    return this.httpClient.put<T>(this.urlApi + path, body, httpOptions)
      .pipe(catchError(this.formatErrors));
  }

  public postOptions<T>(path: string, body: object, newHttpOptions: object): Observable<T> {
    return this.httpClient.post<T>(this.urlApi + path, body, newHttpOptions)
      .pipe(catchError(this.formatErrors));
  }

  public doRequest(request: HttpRequest<any>) {
    return this.httpClient.request(request)
      .pipe(catchError(this.formatErrors));
  }

  private getBasicHttpOptions(): ModelHttpOptions {
    let httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json; charset=utf-8'
      }),
      params: new HttpParams()
    };
    return httpOptions;
  }

  private formatErrors(error: HttpErrorResponse): Observable<any> {

    let message = 'неизвестная ошибка';

    if (error.status == 0)
      message = 'нет ответа от сервера';

    if (error.error)
      if (error.error.message)
        message = error.error.message;

    if (error.status == 403)
      message = '403 - не достаточно прав';

    if (error.status == 401)
      message = '401 - не авторизован';

    if (error.status == 404)
      message = '404 - not found';

    return throwError(() =>
      new HttpErrorResponse({
        error: error.error.message,
        status: error.status,
        statusText: error.statusText
      }));
  }

}
