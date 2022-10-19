import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
// routing
import { ClientAppRoutingModule } from './client-app-routing.module';
// pages
import { ClientAppComponent } from './client-app.component';

@NgModule({
  imports: [
    ClientAppRoutingModule,
    CommonModule
  ],
  declarations: [
    ClientAppComponent
  ]
})
export class ClientAppModule { }
