import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
// guard
import { AuthGuard } from '../authorize/guards/auth.guard';
// pages
import { ClientAppComponent } from './client-app.component';
import { AccrualsPageComponent } from '../accruals-page/accruals-page.component';
import { IndicationsPageComponent } from '../indications-page/indications-page.component';
import { ApplicationsPageComponent } from '../applications-page/applications-page.component';
import { ServicesPageComponent } from '../services-page/services-page.component';
import { NewsPageComponent } from '../news-page/news-page.component';
import { ProfilePageComponent } from '../profile-page/profile-page.component';


const routes: Routes = [
    {
    path: '',
    component: ClientAppComponent,
    canActivate: [AuthGuard],
    children: [
      {path: '', redirectTo: '/news', pathMatch: 'full'},
      {path: 'accruals', component: AccrualsPageComponent,},
      {path: 'indications', component: IndicationsPageComponent,},
      {path: 'applications', component: ApplicationsPageComponent,},
      {path: 'services', component: ServicesPageComponent,},
      {path: 'news', component: NewsPageComponent,},
      {path: 'profile', component: ProfilePageComponent,},
    ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class ClientAppRoutingModule { }
