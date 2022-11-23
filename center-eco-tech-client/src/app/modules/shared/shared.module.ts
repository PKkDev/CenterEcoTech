import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoaderComponent } from './loader/loader.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
  imports: [
    BrowserAnimationsModule,
    CommonModule
  ],
  declarations: [
    LoaderComponent
  ],
  exports: [
    LoaderComponent
  ]
})
export class SharedModule { }
