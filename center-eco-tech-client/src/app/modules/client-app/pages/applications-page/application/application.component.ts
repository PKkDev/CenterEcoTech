import { AfterViewInit, Component, OnDestroy, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { DatePipe } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { AuthService } from 'src/app/modules/authorize/services/auth.service';
import { ApiService } from 'src/app/services/api.service';
import { UserDetailDto } from './domain';

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
  // http
  private userDetailSubs: Subscription;
  private updateDetailSubs: Subscription;
  private ConfirmUserSubs: Subscription;

  constructor(
    protected authService: AuthService,
    private router: Router,
    private aiService: ApiService,
    private _http: HttpClient,
    public datepipe: DatePipe) { }

  ngOnInit() {
    this.applicationsForm = new FormGroup({
      address: new FormGroup({
        city: new FormControl({ value: '', disabled: true }, [Validators.required, Validators.pattern("^([А-Я]{1}[а-яё]{1,29}?-? ?[А-Я]{0,1}[а-яё]{0,29})$")]),
        street: new FormControl ({ value: '', disabled: true }, [Validators.required, Validators.pattern("^([А-Я]{1}[а-яё]{1,29}?-? ?[А-Я]{0,1}[а-яё]{0,29})$")]),
        house: new FormControl ({ value: '', disabled: true }, [Validators.required, Validators.pattern("^([1-9]{1}[0-9]{0,2}[а-я]{0,1})$")]),
        corpus: new FormControl ({ value: '', disabled: true }, [Validators.pattern("^([1-9]{1}[0-9]{0,2}[а-я]{0,1})$")]),
        room: new FormControl ({ value: '', disabled: true }, [Validators.required, Validators.pattern("^([1-9]{1}[0-9]{0,2}[а-я]{0,1})$")]),
      }),
      lastNme: new FormControl ({ value: '', disabled: true }, [Validators.required, Validators.pattern("^([А-Я]{1}[а-яё]{1,40}'?-? ?[А-Я]{0,1}[а-яё]{0,40})$")]),
      firstName: new FormControl ({ value: '', disabled: true }, [Validators.required, Validators.pattern("^([А-Я]{1}[а-яё]{1,39})$")]),
      midName: new FormControl ({ value: '', disabled: true }, [Validators.required, Validators.pattern("^([А-Я]{1}[а-яё]{1,41})$")]),
      phone: new FormControl ({ value: '', disabled: true }, [Validators.required, Validators.pattern(/^[[8|\+7][\- ]?]?[\[?\d{3}\]?[\- ]?]?[\d\- ]{10}$/)]),
      email: new FormControl ({ value: '', disabled: true }, [Validators.required, Validators.email, Validators.maxLength(50), Validators.minLength(3)]),
      theme: new FormControl ({ value: '', disabled: true }, [Validators.required]),
      message: new FormControl ({ value: '', disabled: true }, [Validators.required]),
      //fileappeal: new FormControl ({ value: '', disabled: true },)
    })
    let myDate = new Date();
    console.log(this.datepipe.transform(myDate, 'yyyy-MM-dd'));
  }

  ngAfterViewInit() {
    this.getUserDetail();
  }

  ngOnDestroy() {
    if (this.userDetailSubs) this.userDetailSubs.unsubscribe();
    if (this.updateDetailSubs) this.updateDetailSubs.unsubscribe();
    if (this.ConfirmUserSubs) this.ConfirmUserSubs.unsubscribe();
  }

  private getUserDetail() {
    this.userDetailSubs = this.aiService.get<UserDetailDto>('client/detail')
      .subscribe({
        next: data => { this.userDetaill = data; },
        error: error => { if (this.userDetailSubs) this.userDetailSubs.unsubscribe(); },
        complete: () => { this.feetUserData(); }
      });
  }

  private feetUserData() {
    this.applicationsForm.setValue({
      adress: {
        city: this.userDetaill.adress.city,
        street: this.userDetaill.adress.street,
        house: this.userDetaill.adress.house,
        corpus: this.userDetaill.adress.corpus,
        room: this.userDetaill.adress.room,
      },
      lastNme: this.userDetaill.lastNme,
      firstName: this.userDetaill.firstName,
      midName: this.userDetaill.midName,
      phone: this.userDetaill.phone,
      email: this.userDetaill.email,
      theme: this.userDetaill.theme,
      message: this.userDetaill.message,
      //fileappeal: this.userDetaill.appeal.fileappeal,
      myDate: this.datepipe.transform('yyyy-MM-dd'),
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

  public submit() {
    if (this.applicationsForm.valid) {
      this.updateDetailSubs = this.aiService.post('client/detail', this.applicationsForm.value)
        .subscribe({
          next: data => { },
          error: error => { if (this.updateDetailSubs) this.updateDetailSubs.unsubscribe(); },
          complete: () => { this.getUserDetail() }
        });
      this.userDetailSubs = this.aiService.post('client-request/add-request', this.datepipe.transform)
        .subscribe({
          next: data => { },
          error: error => { if (this.userDetailSubs) this.userDetailSubs.unsubscribe(); },
          complete: () => { this.feetUserData(); }
        });
    }
  }

  public confirmingMessageUser() {
    (window.alert('Заявка отправлена'))
  }

  public reset() {
    this.applicationsForm.reset()
  }

}
