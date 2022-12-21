import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
// pages
import { LogInPageComponent } from './log-in-page/log-in-page.component';
import { RegistrationComponent } from './reg-page/registration.component';
// routing
import { AuthorizeRoutingModule } from './authorize-routing.module';
// guard
import { AuthGuard } from './guards/auth.guard';
// shared
import { SharedModule } from '../shared/shared.module';
// interceptors
import { httpInterceptorProviders } from './interceptors/http-Interceptors';

@NgModule({
  imports: [
    AuthorizeRoutingModule,
    BrowserAnimationsModule,
    FormsModule,
    CommonModule,
    SharedModule
  ],
  declarations: [
    LogInPageComponent,
    RegistrationComponent
  ],
  providers: [
    AuthGuard,
    httpInterceptorProviders
  ]
})
export class AuthorizeModule { }
