import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
// pages
import { RegistrationComponent } from './reg-page/registration.component';
// routing
import { RegRoutingModule } from './registration-routing.module';
// shared
import { SharedModule } from '../shared/shared.module';


@NgModule({
  imports: [
    RegRoutingModule,
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
