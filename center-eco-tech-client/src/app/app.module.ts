import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
// routing
import { AppRoutingModule } from './app-routing.module';
// modules
import { AuthorizeModule } from './modules/authorize/authorize.module';
import { ClientAppModule } from './modules/client-app/client-app.module';
// components
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AccrualsPageComponent } from './modules/accruals-page/accruals-page.component';
import { IndicationsPageComponent } from './modules/indications-page/indications-page.component';
import { ApplicationsPageComponent } from './modules/applications-page/applications-page.component';
import { ServicesPageComponent } from './modules/services-page/services-page.component';
import { NewsPageComponent } from './modules/news-page/news-page.component';
import { ProfilePageComponent } from './modules/profile-page/profile-page.component';

@NgModule({
  declarations: [
    AppComponent,
    AccrualsPageComponent,
    IndicationsPageComponent,
    ApplicationsPageComponent,
    ServicesPageComponent,
    NewsPageComponent,
    ProfilePageComponent
  ],
  imports: [
    ClientAppModule,
    AuthorizeModule,
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    BrowserAnimationsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
