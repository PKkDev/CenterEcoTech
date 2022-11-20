import { Injectable } from '@angular/core';
import { map, of } from 'rxjs';
import { ApiService } from 'src/app/services/api.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private apiService: ApiService) { }

  public login(login: string, pass: string) {

  }

}
