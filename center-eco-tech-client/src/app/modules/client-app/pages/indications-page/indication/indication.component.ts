import { HttpClient } from '@angular/common/http';
import {  AfterViewInit, ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { clientCounterDto } from './domain';
import { ApiService } from 'src/app/services/api.service';
import { UserDetailDto } from './domain';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Subscription } from 'rxjs';

interface type {
  value: string;
  viewValue: string;
}


@Component({
  selector: 'app-indication',
  templateUrl: './indication.component.html',
  styleUrls: ['./indication.component.scss']
})
export class IndicationComponent implements OnInit, AfterViewInit, OnDestroy {

  myDate: Date;
  public indicationForm: FormGroup;
  // data
  public userDetaill: UserDetailDto;
  public name: string | null = null;
  public pastvalue: string | null = null;
  public currentvalue: string | null = null;
  public selectedType: string;

  // http
  private userDetailSubs: Subscription;
  private updateDetailSubs: Subscription;
  // private updateDetailSubs: Subscription;
  // private ConfirmUserSubs: Subscription;
  private addRequestsSubs: Subscription;

  public clientCounterDto: clientCounterDto[] = [];
  public currImg: string;
  public images: any = [];

  private addMeterSups: Subscription;
  private lastMeasureSups: Subscription;
  public adding: boolean = false;
  public selectedMeterName: string = "Горячая вода";
  public message: string | null;

  constructor( private snackBar: MatSnackBar,
    private httpClient: HttpClient,
    private apiService: ApiService){}

  ngOnInit(): void {
  //  this.getLastMeasurement();
    this.getUserDetail();
    this.httpClient.get<clientCounterDto[]>("assets/meters.json").subscribe({
      next : data =>{
      this.clientCounterDto = data
      }
    });
    }

    allType: type[] = [
      { value: '1', viewValue: 'Горячая вода' },
      { value: '2', viewValue: 'Холодная вода' },
      { value: '3', viewValue: 'Газ' },
    ]

    ngAfterViewInit() {
      this.getUserDetail();
    }

    ngOnDestroy() {
      if (this.addRequestsSubs) this.addRequestsSubs.unsubscribe();
  
      // if (this.userDetailSubs) this.userDetailSubs.unsubscribe();
      // if (this.updateDetailSubs) this.updateDetailSubs.unsubscribe();
      // if (this.ConfirmUserSubs) this.ConfirmUserSubs.unsubscribe();
    }

    private getUserDetail() {
      this.userDetailSubs = this.apiService.get<UserDetailDto>('client/detail')
        .subscribe({
          next: data => { this.userDetaill = data; },
          error: error => { if (this.userDetailSubs) this.userDetailSubs.unsubscribe(); },
          complete: () => { }
        });
    }


    public formEnable() {
      this.indicationForm.enable();
    }
  
    public formDisable() {
      this.indicationForm.disable();
    }
  
    public onCreateRequestClick() {
      debugger;
      if (!this.name || !this.name) return;
      this.addRequestsSubs = this.apiService.post('measurement/add-measurement', this.indicationForm.value)
        .subscribe({
          next: data => {
            this.snackBar.open('успешно', 'OK');
            this.name = null;
            this.currentvalue = null;
          },
          error: error => {
            if (this.addRequestsSubs) this.addRequestsSubs.unsubscribe();
          },
          complete: () => { }
        });
    }
  
    public checkFields() {
      const check = !this.name || !this.name;
      return check;
    }

    public confirmingMessageUser() {
      (window.alert('Показания отправлены'))
    }
  
    public reset() {
      this.indicationForm.reset()
    }

  public activateAddingMeter(): void{
    this.adding = true;
  }

  private getLastMeasurement() {
    this.lastMeasureSups = this.apiService.post('measurement/get-last-measurement')
    .subscribe({
      next: data => { 
        console.log(data);
      },
          error: error => { if (this.lastMeasureSups) this.lastMeasureSups.unsubscribe(); this.message = error.error; },
          complete: () => { }
    })
  }

  private addMeter() {
    this.message = null;
    const httpBody = {
      name : this.selectedMeterName
    };
    this.addMeterSups = this.apiService.post('measurement/add-new-counter', httpBody)
    .subscribe({
      next: data => { },
          error: error => { if (this.addMeterSups) this.addMeterSups.unsubscribe(); this.message = error.error; },
          complete: () => { this.message = "Счётчик успешно добавлен" }
    })
  }

  public onAddCLick() {
    // this.adding = false;
    this.addMeter();
  }

  public onReadyCLick() {
    this.message = null;
    this.adding = false;
  }

  }