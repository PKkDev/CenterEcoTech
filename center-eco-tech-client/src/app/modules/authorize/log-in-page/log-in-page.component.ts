import { AfterViewInit, ChangeDetectorRef, Component, OnDestroy, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { ApiService } from 'src/app/services/api.service';
import { AuthService } from '../services/auth.service';
import { LogInPageViewType } from './model';

@Component({
  selector: 'app-log-in-page',
  templateUrl: './log-in-page.component.html',
  styleUrls: ['./log-in-page.component.scss']
})
export class LogInPageComponent implements OnInit, AfterViewInit, OnDestroy {

  // template
  @ViewChild('inputPhoneTemplate') inputPhoneTemplate: TemplateRef<any>;
  @ViewChild('inputCodeTemplate') inputCodeTemplate: TemplateRef<any>;
  public logInPageViewType: LogInPageViewType = LogInPageViewType.Phone;
  public nowTemplate: TemplateRef<any>;
  // field
  public phone: string;
  public acceptCode: string;
  // flags
  public codeIsCheck = false;
  public codeIsSend = false;
  // http
  public message: string | null;
  private sendSmsSubs: Subscription;
  private checkCodeubs: Subscription;

  constructor(
    private cdr: ChangeDetectorRef,
    private router: Router,
    private apiService: ApiService,
    private authService: AuthService) { }

  ngOnInit() { }

  ngAfterViewInit() {
    this.onLoadTemplate();
  }

  ngOnDestroy() {
    if (this.sendSmsSubs) this.sendSmsSubs.unsubscribe();
    if (this.checkCodeubs) this.checkCodeubs.unsubscribe();
  }

  public onNextCLick() {
    this.logInPageViewType = LogInPageViewType.Code;
    this.onLoadTemplate();
    this.sendCodeToPhone();
  }

  public onChangePhoneClick() {
    this.logInPageViewType = LogInPageViewType.Phone;
    this.onLoadTemplate();
  }

  public onLoadTemplate() {
    switch (this.logInPageViewType) {
      case LogInPageViewType.Phone: this.nowTemplate = this.inputPhoneTemplate; break;
      case LogInPageViewType.Code: this.nowTemplate = this.inputCodeTemplate; break;
      default: this.nowTemplate = this.inputPhoneTemplate; break;
    }
    this.cdr.detectChanges();
  }

  public onReSendCodeToPhoneClick() {
    this.sendCodeToPhone();
  }

  onCheckCodeClick() {
    this.message = null;
    this.codeIsCheck = true;
    this.checkCodeubs = this.authService.checkCode(this.acceptCode, this.phone)
      .subscribe({
        next: data => { this.codeIsCheck = false; this.router.navigate(['']); },
        error: error => { this.codeIsCheck = false; this.message = error.error; }
      });
  }

  public sendCodeToPhone() {
    this.message = null;
    this.codeIsSend = true;
    this.sendSmsSubs = this.authService.sendCodeToPhone(this.phone)
      .subscribe({
        next: data => { this.codeIsSend = false; },
        error: error => {
          this.message = error.error;
          this.onChangePhoneClick();
          this.codeIsSend = false;
        }
      });
  }

}
