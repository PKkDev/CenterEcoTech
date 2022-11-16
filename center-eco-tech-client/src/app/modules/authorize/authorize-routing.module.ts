import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
// pages
import { LogInPageComponent } from './log-in-page/log-in-page.component';

const routes: Routes = [
    { path: 'login', component: LogInPageComponent }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class AuthorizeRoutingModule { }
