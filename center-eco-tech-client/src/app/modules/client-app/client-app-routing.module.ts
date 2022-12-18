import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
// guard
import { AuthGuard } from '../authorize/guards/auth.guard';
// pages
import { ClientAppComponent } from './client-app.component';
import { AccrualsPageComponent } from './pages/accruals-page/accruals-page.component';
import { ApplicationsPageComponent } from './pages/applications-page/applications-page.component';
import { ApplicationComponent } from './pages/applications-page/application/application.component';
import { ApplicationsHistoryComponent } from './pages/applications-page/applications-history/applications-history.component';
import { IndicationsPageComponent } from './pages/indications-page/indications-page.component';
import { NewsPageComponent } from './pages/news-page/news-page.component';
import { ProfilePageComponent } from './pages/profile-page/profile-page.component';
import { ServicesPageComponent } from './pages/services-page/services-page.component';

const routes: Routes = [
  {
    path: '',
    component: ClientAppComponent,
    canActivate: [AuthGuard],
    children: [
      { path: '', redirectTo: '/news', pathMatch: 'full' },
      { path: 'accruals', component: AccrualsPageComponent, },
      { path: 'indications', component: IndicationsPageComponent, },
      { path: 'applications', component: ApplicationsPageComponent, children: [
        { path: 'create-application', component: ApplicationComponent, },
        { path: 'history', component: ApplicationsHistoryComponent, }
      ] },
      { path: 'services', component: ServicesPageComponent, },
      { path: 'news', component: NewsPageComponent, },
      { path: 'profile', component: ProfilePageComponent, },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ClientAppRoutingModule { }
