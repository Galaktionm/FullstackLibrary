import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthorizationModule } from './authorization/authorization.module';
import { FunctionalityModule } from './functionality/functionality.module';
import { AuthInterceptor } from './authorization/AuthInterceptor';
import { CoreModule } from './core/core.module';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    AuthorizationModule,
    FunctionalityModule,
    CoreModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
