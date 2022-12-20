import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
// pages
import { RegistrationComponent } from './reg-page/registration.component';
// shared
import { SharedModule } from '../shared/shared.module';


@NgModule({
  imports: [
    BrowserAnimationsModule,
    FormsModule,
    CommonModule,
    SharedModule
  ],
  declarations: [
    RegistrationComponent
  ],
  providers: [
  ]
  
})
export class RegistrationModule { }
