import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, FormGroupName } from '@angular/forms';

@Component({
  selector: 'app-profile-page',
  templateUrl: './profile-page.component.html',
  styleUrls: ['./profile-page.component.scss']
})
export class ProfilePageComponent implements OnInit {
  buttonIsClick() {
    this.isCollapsed = !this.isCollapsed
  }
  public profileForm: FormGroup;
  public isCollapsed: boolean = true;
  constructor() {}
  ngOnInit() {
    this.profileForm = new FormGroup({
      surname: new FormControl ({ value: '', disabled: true }, [Validators.required, Validators.pattern("^([А-Я]{1}[а-яё]{1,40}'?-? ?[А-Я]{0,1}[а-яё]{0,40})$")]),
      name: new FormControl ({ value: '', disabled: true }, [Validators.required, Validators.pattern("^([А-Я]{1}[а-яё]{1,39})$")]),
      secondName: new FormControl ({ value: '', disabled: true }, [Validators.required, Validators.pattern("^([А-Я]{1}[а-яё]{1,41})$")]),
      address: new FormGroup({
        street: new FormControl ({ value: '', disabled: true }, [Validators.required, Validators.pattern("^([А-Я]{1}[а-яё]{1,29}?-? ?[А-Я]{0,1}[а-яё]{0,29})$")]),
        house: new FormControl ({ value: '', disabled: true }, [Validators.required, Validators.pattern("^([1-9]{1}[0-9]{0,2}[а-я]{0,1})$")]),
        corpus: new FormControl ({ value: '', disabled: true }, [Validators.pattern("^([1-9]{1}[0-9]{0,2}[а-я]{0,1})$")]),
        room: new FormControl ({ value: '', disabled: true }, [Validators.required, Validators.pattern("^([1-9]{1}[0-9]{0,2}[а-я]{0,1})$")]),
      }),
      numberPersonalAccount: new FormControl ({ value: '', disabled: true }, [Validators.required, Validators.pattern("^([0-9]{10})$")]),
      email: new FormControl ({ value: '', disabled: true }, [Validators.required, Validators.email, Validators.maxLength(50), Validators.minLength(3)]),
    })
  }
  formEnable() {
    this.profileForm.enable();
  }
  formDisable() {
    this.profileForm.disable();
  }
  submit() {
    if (this.profileForm.valid) {
      console.log( 'Form submitted: ', this.profileForm )
      const formData = { ...this.profileForm.value }
      console.log('Form Data:', formData)
    }
  }
  reset() {
    this.profileForm.reset()
  }
}
