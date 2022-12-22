import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoaderComponent } from './loader/loader.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { EmptyBannerComponent } from './empty-banner/empty-banner.component';

@NgModule({
  imports: [
    BrowserAnimationsModule,
    CommonModule
  ],
  declarations: [
    EmptyBannerComponent,
    LoaderComponent
  ],
  exports: [
    EmptyBannerComponent,
    LoaderComponent
  ]
})
export class SharedModule { }
