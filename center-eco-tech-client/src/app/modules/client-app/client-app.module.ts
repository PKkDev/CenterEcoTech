import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DatePipe } from '@angular/common';
// material
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
// routing
import { ClientAppRoutingModule } from './client-app-routing.module';
// layout
import { HeaderComponent } from './layouts/site-layout/header.component';
// pages
import { ClientAppComponent } from './client-app.component';
import { ServicesPageComponent } from './pages/services-page/services-page.component';
import { ProfilePageComponent } from './pages/profile-page/profile-page.component';
import { NewsPageComponent } from './pages/news-page/news-page.component';
import { MainArticleComponent } from './pages/news-page/main-article/main-article.component';
import { ArticleComponent } from './pages/news-page/article/article.component';
import { IndicationsPageComponent } from './pages/indications-page/indications-page.component';
import { IndicationComponent } from './pages/indications-page/indication/indication.component';
import { IndicationsHistoryComponent } from './pages/indications-page/indications-history/indications-history.component';
import { ApplicationsPageComponent } from './pages/applications-page/applications-page.component';
import { AccrualsPageComponent } from './pages/accruals-page/accruals-page.component';
import { ApplicationsHistoryComponent } from './pages/applications-page/applications-history/applications-history.component';
import { ApplicationComponent } from './pages/applications-page/application/application.component';
// pipe
import { ApplictionStatusConverterPipe } from './pages/applications-page/applications-history/appliction-status-converter.pipe';
import { IndicationsTypeConverterPipe } from './pages/indications-page/indications-history/indications-type-converter.pipe';
// shared
import { SharedModule } from '../shared/shared.module';

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
    MatSelectModule,
    MatSnackBarModule,
    MatDatepickerModule,
    MatNativeDateModule,
    SharedModule
  ],
  declarations: [
    ClientAppComponent,
    HeaderComponent,
    AccrualsPageComponent,
    ServicesPageComponent,
    ProfilePageComponent,
    NewsPageComponent,
    MainArticleComponent,
    ArticleComponent,
    IndicationsPageComponent,
    IndicationComponent,
    IndicationsHistoryComponent,
    ApplicationsPageComponent,
    ApplicationComponent,
    ApplicationsHistoryComponent,
    ApplictionStatusConverterPipe,
    IndicationsTypeConverterPipe
  ],
  providers: [DatePipe]
})
export class ClientAppModule { }
