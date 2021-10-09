import { Component, HostListener, OnDestroy, OnInit } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { GalleryOdataService } from './services/gallery-odata.service';
import { IImage } from './Utils/interfaces';
import { takeUntil } from 'rxjs/operators';
import { ODataPagedResult } from 'angular-odata-es5';
import { GalleryService } from './services/gallery.service';

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
  public images$: Observable<IImage[]> | undefined;
  public imagesCount = 0;
  public defaultImage = 'https://st4.depositphotos.com/14953852/22772/v/600/depositphotos_227725184-stock-illustration-image-available-icon-flat-vector.jpg';

  constructor(
    public galleryOdataService: GalleryOdataService,
    public galleryService: GalleryService
  ) { }

  ngOnInit(): void {
    this.getImages();
  }

  ngOnDestroy(): void {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }

  getImages(): void {
    this.images$ = this.galleryService.getImages();
  }

  getImagesFromArray(images: IImage[]): string[] {
    let arr: string[] = [];
    images.map(s => arr = [...arr, ...s.image_List_Array] );
    return arr;
  }

  @HostListener('scroll', ['$event'])
  onScroll(event: any): void {
      // visible height + pixel scrolled >= total height 
      if (event.target.offsetHeight + event.target.scrollTop >= event.target.scrollHeight) {
        console.log('End');
      }
  }

}
