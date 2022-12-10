import { AfterViewInit, Component, OnDestroy, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, FormGroupName } from '@angular/forms';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { AuthService } from 'src/app/modules/authorize/services/auth.service';
import { ApiService } from 'src/app/services/api.service';
import { UserDetailDto } from './domain';

@Component({
  selector: 'app-profile-page',
  templateUrl: './profile-page.component.html',
  styleUrls: ['./profile-page.component.scss']
})
export class ProfilePageComponent implements OnInit, AfterViewInit, OnDestroy {

  public profileForm: FormGroup;
  public isCollapsed: boolean = true;
  // data
  public userDetaill: UserDetailDto;
  // http
  private userDetailSubs: Subscription;
  private updateDetailSubs: Subscription;
  private DeleteUserSubs: Subscription;

  constructor(
    protected authService: AuthService,
    private router: Router,
    private aiService: ApiService) { }

  ngOnInit() {
    this.profileForm = new FormGroup({
      lastNme: new FormControl({ value: '', disabled: true }, [Validators.required, Validators.pattern("^([А-Я]{1}[а-яё]{1,40}'?-? ?[А-Я]{0,1}[а-яё]{0,40})$")]),
      firstName: new FormControl({ value: '', disabled: true }, [Validators.required, Validators.pattern("^([А-Я]{1}[а-яё]{1,39})$")]),
      midName: new FormControl({ value: '', disabled: true }, [Validators.required, Validators.pattern("^([А-Я]{1}[а-яё]{1,41})$")]),
      adress: new FormGroup({
        city: new FormControl({ value: '', disabled: true }, [Validators.required, Validators.pattern("^([А-Я]{1}[а-яё]{1,29}?-? ?[А-Я]{0,1}[а-яё]{0,29})$")]),
        street: new FormControl({ value: '', disabled: true }, [Validators.required, Validators.pattern("^([А-Я]{1}[а-яё]{1,29}?-? ?[А-Я]{0,1}[а-яё]{0,29})$")]),
        house: new FormControl({ value: '', disabled: true }, [Validators.required, Validators.pattern("^([1-9]{1}[0-9]{0,2}[а-я]{0,1})$")]),
        corpus: new FormControl({ value: '', disabled: true }, [Validators.pattern("^([1-9]{1}[0-9]{0,2}[а-я]{0,1})$")]),
        room: new FormControl({ value: '', disabled: true }, [Validators.required, Validators.pattern("^([1-9]{1}[0-9]{0,2}[а-я]{0,1})$")]),
      }),
      phone: new FormControl({ value: '', disabled: true }, [Validators.required, Validators.pattern("^([0-9]{11})$")]),
      email: new FormControl({ value: '', disabled: true }, [Validators.required, Validators.email, Validators.maxLength(50), Validators.minLength(3)]),
    })
  }

  ngAfterViewInit() {
    this.getUserDetail();
  }

  ngOnDestroy() {
    if (this.userDetailSubs) this.userDetailSubs.unsubscribe();
    if (this.updateDetailSubs) this.updateDetailSubs.unsubscribe();
    if (this.DeleteUserSubs) this.DeleteUserSubs.unsubscribe();
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
    this.profileForm.setValue({
      lastNme: this.userDetaill.lastNme,
      firstName: this.userDetaill.firstName,
      midName: this.userDetaill.midName,
      adress: {
        city: this.userDetaill.adress.city,
        street: this.userDetaill.adress.street,
        house: this.userDetaill.adress.house,
        corpus: this.userDetaill.adress.corpus,
        room: this.userDetaill.adress.room,
      },
      phone: this.userDetaill.phone,
      email: this.userDetaill.email
    });
  }

  public buttonIsClick() {
    this.isCollapsed = !this.isCollapsed
  }

  public formEnable() {
    this.profileForm.enable();
  }

  public formDisable() {
    this.profileForm.disable();
  }

  public submit() {
    if (this.profileForm.valid) {
      this.updateDetailSubs = this.aiService.post('client/detail', this.profileForm.value)
        .subscribe({
          next: data => { },
          error: error => { if (this.updateDetailSubs) this.updateDetailSubs.unsubscribe(); },
          complete: () => { this.getUserDetail(); }
        });
    }
  }

  public reset() {
    this.profileForm.reset();
    this.feetUserData();
  }

  public onDeleteUser() {
    if (window.confirm('удалить пользователя?')) {
      this.DeleteUserSubs = this.aiService.delete('client/')
        .subscribe({
          next: data => { },
          error: error => { if (this.DeleteUserSubs) this.DeleteUserSubs.unsubscribe(); },
          complete: () => { this.authService.logOut(); this.router.navigate(['']); }
        });
    }
  }

}
