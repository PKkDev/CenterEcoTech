import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {DatePipe} from '@angular/common';
// material
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorIntl, MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
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
import { ApplicationsHistoryComponent } from './pages/applications-page/applications-history/applications-history.component';
import { ApplicationComponent } from './pages/applications-page/application/application.component';


@NgModule({
  imports: [
    ClientAppRoutingModule,
    CommonModule,
    MatIconModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatFormFieldModule,
    MatInputModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  declarations: [
    ClientAppComponent,
    HeaderComponent,
    ServicesPageComponent,
    ProfilePageComponent,
    NewsPageComponent,
    IndicationsPageComponent,
    ApplicationsPageComponent,
    AccrualsPageComponent,
    ApplicationsHistoryComponent,
    ApplicationComponent
  ],
  providers: [DatePipe]
})
export class ClientAppModule { }
