import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
// pages
import { LogInPageComponent } from './log-in-page/log-in-page.component';
// routing
import { AuthorizeRoutingModule } from './authorize-routing.module';
// guard
import { AuthGuard } from './guards/auth.guard';
// shared
import { SharedModule } from '../shared/shared.module';

@NgModule({
  imports: [
    AuthorizeRoutingModule,
    BrowserAnimationsModule,
    FormsModule,
    CommonModule,
    SharedModule
  ],
  declarations: [
    LogInPageComponent
  ],
  providers: [
    AuthGuard
  ]
})
export class AuthorizeModule { }
