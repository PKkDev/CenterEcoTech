import { AfterViewInit, Component, OnDestroy, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Subscription } from 'rxjs';
import { ApiService } from 'src/app/services/api.service';
import { UserDetailDto } from './domain';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-application',
  templateUrl: './application.component.html',
  styleUrls: ['./application.component.scss']
})
export class ApplicationComponent implements OnInit, AfterViewInit, OnDestroy {

  myDate: Date;
  public applicationsForm: FormGroup;
  public isCollapsed: boolean = true;
  // data
  public userDetaill: UserDetailDto;
  public theme: string | null = null;
  public message: string | null = null;
  // http
  private userDetailSubs: Subscription;
  // private updateDetailSubs: Subscription;
  // private ConfirmUserSubs: Subscription;
  private addRequestsSubs: Subscription;

  constructor(
    private snackBar: MatSnackBar,
    private apiService: ApiService) { }

  ngOnInit() {
    this.getUserDetail();
  }

  ngAfterViewInit() {
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

  public buttonIsClick() {
    this.isCollapsed = !this.isCollapsed
  }

  public formEnable() {
    this.applicationsForm.enable();
  }

  public formDisable() {
    this.applicationsForm.disable();
  }

  public onCreateRequestClick() {
    debugger;
    if (!this.theme || !this.message) return;
    const httpBody = {
      theme: this.theme,
      message: this.message
    };
    this.addRequestsSubs = this.apiService.post('client-request/add-request', httpBody)
      .subscribe({
        next: data => {
          this.snackBar.open('успешно', 'OK');
          this.theme = null;
          this.message = null;
        },
        error: error => {
          if (this.addRequestsSubs) this.addRequestsSubs.unsubscribe();
        },
        complete: () => { }
      });
  }

  public checkFields() {
    const check = !this.theme || !this.message;
    return check;
  }

  // public submit() {
  //   if (this.applicationsForm.valid) {
  //     this.updateDetailSubs = this.aiService.post('client/detail', this.applicationsForm.value)
  //       .subscribe({
  //         next: data => { },
  //         error: error => { if (this.updateDetailSubs) this.updateDetailSubs.unsubscribe(); },
  //         complete: () => { this.getUserDetail() }
  //       });
  //     this.userDetailSubs = this.aiService.post('client-request/add-request', this.datepipe.transform)
  //       .subscribe({
  //         next: data => { },
  //         error: error => { if (this.userDetailSubs) this.userDetailSubs.unsubscribe(); },
  //         complete: () => { this.feetUserData(); }
  //       });
  //   }
  // }

  public confirmingMessageUser() {
    (window.alert('Заявка отправлена'))
  }

  public reset() {
    this.applicationsForm.reset()
  }

}
