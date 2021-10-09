import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { GalleryOdataService } from './services/gallery-odata.service';
import { IImage } from './Utils/interfaces';
import { takeUntil } from 'rxjs/operators';

@Component({
  // tslint:disable-next-line: component-selector
  selector: 'to-gallery',
  templateUrl: './gallery.component.html',
  styleUrls: ['./gallery.component.css']
})
export class GalleryComponent implements OnInit, OnDestroy {

  private unsubscribe$: Subject<any> = new Subject<any>();

  private query = '';
  private orderBy = '';
  private skip = 0;

  public itemsPerPage = 30;
  public images: IImage[] = [];
  public imagesCount = 0;

  constructor(
    public galleryOdataService: GalleryOdataService
  ) { }

  ngOnInit(): void {
  }

  ngOnDestroy(): void {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }

  getImages(): void {
    this.galleryOdataService.getImages(this.skip, this.itemsPerPage, this.query, this.orderBy)
    .pipe(takeUntil(this.unsubscribe$))
    .subscribe(result => {
      this.images = result.data;
      this.imagesCount = result.count;
      console.log(this.images);
    }, error => {
      console.error(error);   // Local error handler
    });
  }

}
