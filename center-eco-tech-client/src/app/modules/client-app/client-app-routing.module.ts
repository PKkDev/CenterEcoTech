import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
// guard
import { AuthGuard } from '../authorize/guards/auth.guard';
// pages
import { ClientAppComponent } from './client-app.component';
import { SiteLayoutComponent } from '../layouts/site-layout/site-layout.component';


const routes: Routes = [
    {
        path: '',
        component: ClientAppComponent,
        canActivate: [AuthGuard],
        children: [
          {
            path: '',
            component: SiteLayoutComponent,
            children: [ ]
          }
        ]
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class ClientAppRoutingModule { }
