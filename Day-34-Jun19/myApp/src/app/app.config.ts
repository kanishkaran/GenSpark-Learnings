import { ApplicationConfig, provideBrowserGlobalErrorListeners, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideHttpClient } from '@angular/common/http';
import { ProductService } from './services/productService';
import { LoginService } from './services/loginService';
import { AuthGuard } from './auth-guard';
import { provideState, provideStore } from '@ngrx/store';
import { userReducer } from './ngrx/users.reducer';






export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideHttpClient(),
    provideStore(),
    provideState('user', userReducer),
    ProductService,
    LoginService,
    AuthGuard
  ]
};
