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
import { IndicationComponent } from './pages/indications-page/indication/indication.component';
import { IndicationsHistoryComponent } from './pages/indications-page/indications-history/indications-history.component';
import { NewsPageComponent } from './pages/news-page/news-page.component';
import { MainArticleComponent } from './pages/news-page/main-article/main-article.component';
import { ArticleComponent } from './pages/news-page/article/article.component';
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
      {
        path: 'indications',
        component: IndicationsPageComponent,
        children: [
          { path: '', pathMatch: 'full', redirectTo: 'history' },
          { path: 'submit-indication', component: IndicationComponent, },
          { path: 'history', component: IndicationsHistoryComponent, }
        ]
      },
      {
        path: 'applications',
        component: ApplicationsPageComponent,
        children: [
          { path: '', pathMatch: 'full', redirectTo: 'history' },
          { path: 'create-application', component: ApplicationComponent, },
          { path: 'history', component: ApplicationsHistoryComponent, }
        ]
      },
      { path: 'services', component: ServicesPageComponent, },
      { path: 'news', component: NewsPageComponent, },
      { path: 'news/main-article', component: MainArticleComponent, },
      { path: 'news/article', component: ArticleComponent, },
      { path: 'profile', component: ProfilePageComponent, },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ClientAppRoutingModule { }
