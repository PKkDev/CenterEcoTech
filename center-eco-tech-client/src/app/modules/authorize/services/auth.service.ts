import { EventEmitter, Injectable } from '@angular/core';
import { BehaviorSubject, map, Observable, of } from 'rxjs';
import { ApiService } from 'src/app/services/api.service';
import { LoginHttpResponse } from '../log-in-page/model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private user: BehaviorSubject<LoginHttpResponse>;
  private isLogged = new EventEmitter<boolean>();

  constructor(private apiService: ApiService) {

    const token = sessionStorage.getItem('token');

    if (token)
      this.user = new BehaviorSubject<LoginHttpResponse>({ token: token });
    else
      this.user = new BehaviorSubject<LoginHttpResponse>({ token: null });

  }

  public checkCode(code: string, phone: string): Observable<LoginHttpResponse> {
    const httpBody = {
      code: code,
      phone: phone
    }
    return this.apiService.post<LoginHttpResponse>('сlient/check-sms', httpBody)
      .pipe(
        map((data: LoginHttpResponse) => {
          if (data) {
            if (data.token) {
              this.user.next({ token: data.token });
              sessionStorage.setItem('token', data.token);
              this.isLogged.emit(true);
            }
          }
          return data;
        })
      );
  }

  public sendCodeToPhone(phone: string): Observable<any> {
    const httpBody = {
      phone: phone
    };
    return this.apiService.post('сlient/send-sms', httpBody);
  }

}
