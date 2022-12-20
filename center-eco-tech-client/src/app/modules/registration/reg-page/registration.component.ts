import { AfterViewInit, Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';

import { ApiService } from 'src/app/services/api.service';
import { CooperativeDto } from './domain';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit, AfterViewInit {

  constructor( private apiService: ApiService) { }

  ngOnInit(): void {
  }

  // fields
  public phone: string;
  public coops: CooperativeDto[] = [];
  // data
  public coopData: CooperativeDto[] = [];
  // http
  private coopDetailSubs: Subscription;


  public onNextCLick() {
  }

  ngAfterViewInit() {
    this.getCooperatives();
  } 

  private getCooperatives() {
    this.coopDetailSubs = this.apiService.get<CooperativeDto[]>('cooperative')
      .subscribe({
        next: data => { this.coopData = data; 
        },
        error: error => { if (this.coopDetailSubs) this.coopDetailSubs.unsubscribe(); },
        complete: () => { this.feetCooperatives(); }
      });
  }

  private feetCooperatives() {      
    this.coopData.forEach(element => {
      this.coops.push(element);
    });

  }

  public submit() {
  }

}
