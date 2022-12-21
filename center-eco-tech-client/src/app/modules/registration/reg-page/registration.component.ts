import { AfterViewInit, Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';

import { ApiService } from 'src/app/services/api.service';
import { CooperativeDto } from './domain';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit, AfterViewInit {

  constructor(  
    private apiService: ApiService, 
    private router: Router) { }

  ngOnInit(): void {
  }

  // fields
  public phone: string;
  public name: string;
  public coops: CooperativeDto[] = [];
  public selectedCoop: number;
  // data
  public coopData: CooperativeDto[] = [];
  // http
  private coopDetailSubs: Subscription;
  private userRegSups: Subscription;
  public message: string | null;


  public onNextCLick() {
    this.message = null;
    this.registerUser();
  }

  ngAfterViewInit() {
    this.getCooperatives();
  } 

  private getCooperatives() {
    this.coopDetailSubs = this.apiService.get<CooperativeDto[]>('cooperative')
      .subscribe({
        next: data => { this.coopData = data; 
        },
        error: error => { 
          if (this.coopDetailSubs) this.coopDetailSubs.unsubscribe();
          this.message = error.error;
         },
        complete: () => { this.feetCooperatives(); }
      });
  }

  private feetCooperatives() {      
    this.coopData.forEach(element => {
      this.coops.push(element);
    });

  }

  private registerUser() {
    const httpBody = {
      cooperativeId: this.selectedCoop,
      phone: this.phone,
      firstName: this.name
    };
    if (this.selectedCoop != null && this.phone != null && this.name != null) {
      this.userRegSups = this.apiService.post<RegistrationDto>('client/register', httpBody)
      .subscribe({
        next: data => {   this.router.navigate(['']);  },
        error: error => {
          if (this.userRegSups) this.userRegSups.unsubscribe();
          this.message = error.error;
        },
        complete: () => { }
      })
    }
    else {
      this.message = "Заполните все поля";
    }
   };

  public submit() {
  }

}

export class RegistrationDto {
  public name: number;
  public phone: string;
  public firstName: string;
}

