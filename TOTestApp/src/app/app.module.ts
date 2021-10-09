import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { GalleryModule } from './gallery/gallery.module';
import { APP_CONFIG } from 'src/config/app.config';
import { environment } from 'src/environments/environment';
import { ODataConfiguration, ODataServiceFactory } from 'angular-odata-es5';
import { ODataConfig } from './odata-config';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    GalleryModule
  ],
  providers: [
    {
      provide: APP_CONFIG,
      useValue: environment.configuration
    },
    ODataServiceFactory,
    {
      provide: ODataConfiguration,
      useClass: ODataConfig
    },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
