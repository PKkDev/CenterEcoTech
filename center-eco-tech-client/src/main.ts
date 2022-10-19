import { enableProdMode, StaticProvider } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';
import { environment } from './environments/environment';

export function getProdUrl() {
  const baseUrl = document.getElementsByTagName('base')[0].href;
  return baseUrl + 'api/'
}

if (environment.production) {
  enableProdMode();
}

export function getBaseUrl() {
  if (environment.production) {
    return getProdUrl();
  } else {
    // return getProdUrl();
    return 'https://localhost:44354/api/';
  }
}

const providers: StaticProvider[] = [
  {
    provide: 'BASE_APP_URL',
    useFactory: getBaseUrl,
    deps: []
  }
];

platformBrowserDynamic(providers).bootstrapModule(AppModule)
  .catch(err => console.error(err));
