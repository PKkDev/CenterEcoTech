import { AfterViewInit, Component, OnDestroy, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Subscription } from 'rxjs';
import { ApiService } from 'src/app/services/api.service';
import { UserDetailDto } from './domain';
import { MatSnackBar } from '@angular/material/snack-bar';

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

  constructor(
    private snackBar: MatSnackBar,
    private apiService: ApiService) { }

  ngOnInit() {
    this.getUserDetail();
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

}
