import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
// routing
import { ClientAppRoutingModule } from './client-app-routing.module';
// layout
import { HeaderComponent } from './layouts/site-layout/header.component';
// pages
import { ClientAppComponent } from './client-app.component';
import { ServicesPageComponent } from './pages/services-page/services-page.component';
import { ProfilePageComponent } from './pages/profile-page/profile-page.component';
import { NewsPageComponent } from './pages/news-page/news-page.component';
import { IndicationsPageComponent } from './pages/indications-page/indications-page.component';
import { ApplicationsPageComponent } from './pages/applications-page/applications-page.component';
import { AccrualsPageComponent } from './pages/accruals-page/accruals-page.component';

@NgModule({
  imports: [
    ClientAppRoutingModule,
    CommonModule,
    MatIconModule
  ],
  declarations: [
    ClientAppComponent,
    HeaderComponent,
    ServicesPageComponent,
    ProfilePageComponent,
    NewsPageComponent,
    IndicationsPageComponent,
    ApplicationsPageComponent,
    AccrualsPageComponent
  ]
})
export class ClientAppModule { }
