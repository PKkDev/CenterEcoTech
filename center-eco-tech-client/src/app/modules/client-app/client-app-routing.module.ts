import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
// guard
import { AuthGuard } from '../authorize/guards/auth.guard';
// pages
import { ClientAppComponent } from './client-app.component';

const routes: Routes = [
    {
        path: '',
        component: ClientAppComponent,
        canActivate: [AuthGuard],
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class ClientAppRoutingModule { }
