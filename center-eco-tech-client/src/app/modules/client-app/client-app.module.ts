import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {MatIconModule} from '@angular/material/icon';
// routing
import { ClientAppRoutingModule } from './client-app-routing.module';
// pages
import { ClientAppComponent } from './client-app.component';
// layout
import { HeaderComponent } from '../layouts/site-layout/header.component';


@NgModule({
  imports: [
    ClientAppRoutingModule,
    CommonModule,
    MatIconModule
  ],
  declarations: [
    ClientAppComponent,
    HeaderComponent
  ]
})
export class ClientAppModule { }
