import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GalleryComponent } from './gallery.component';
import { GalleryService } from './services/gallery.service';
import { GalleryOdataService } from './services/gallery-odata.service';



@NgModule({
  declarations: [GalleryComponent],
  imports: [
    CommonModule
  ],
  providers: [
    GalleryService,
    GalleryOdataService
  ]
})
export class GalleryModule { }
