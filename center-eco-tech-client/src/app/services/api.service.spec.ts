import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { TestBed, inject } from '@angular/core/testing';
import { ApiService } from './api.service';
import { defer } from 'rxjs';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

describe('Api service', () => {

  let apiService: ApiService;
  let httpClientSpy: jasmine.SpyObj<HttpClient>;

  beforeEach(() => {
    httpClientSpy = jasmine.createSpyObj('HttpClient', ['get']);
    apiService = new ApiService(httpClientSpy, '');
  });

  it('should return an error when the server returns a 404', (done: DoneFn) => {
    const errorResponse = new HttpErrorResponse({
      error: 'test 404 error',
      status: 404,
      statusText: 'Not Found'
    });

    httpClientSpy.get.and.returnValue(asyncError(errorResponse));

    apiService.get('not-founde-test').subscribe({
      next: data => done.fail('expected an error, not heroes'),
      error: error => {
        console.log(error);

        expect(error.error).toContain('404 error');
        done();
      }
    });
  });

});


export function asyncData<T>(data: T) {
  return defer(() => Promise.resolve(data));
}

export function asyncError<T>(errorObject: any) {
  return defer(() => Promise.reject(errorObject));
}
