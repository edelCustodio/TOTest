import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GalleryComponent } from './gallery.component';
import { GalleryService } from './services/gallery.service';
import { GalleryOdataService } from './services/gallery-odata.service';
import { LazyLoadImageModule } from 'ng-lazyload-image';



@NgModule({
  declarations: [GalleryComponent],
  imports: [
    CommonModule,
    LazyLoadImageModule
  ],
  providers: [
    GalleryService,
    GalleryOdataService
  ],
  exports: [
    GalleryComponent
  ]
})
export class GalleryModule { }
