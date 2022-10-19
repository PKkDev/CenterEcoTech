import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
// pages
import { LogInPageComponent } from './log-in-page/log-in-page.component';
// routing
import { AuthorizeRoutingModule } from './authorize-routing.module';
// guard
import { AuthGuard } from './guards/auth.guard';

@NgModule({
  imports: [
    AuthorizeRoutingModule,
    FormsModule,
    CommonModule
  ],
  declarations: [
    LogInPageComponent
  ],
  providers: [
    AuthGuard
  ]
})
export class AuthorizeModule { }
