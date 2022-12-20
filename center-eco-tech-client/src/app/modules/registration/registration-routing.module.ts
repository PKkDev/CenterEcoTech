import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
// pages
import { RegistrationComponent } from './reg-page/registration.component';
  

const routes: Routes = [
    { 
        path: 'registration',
        component: RegistrationComponent,
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class RegRoutingModule { }