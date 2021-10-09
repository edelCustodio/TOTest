import { Injectable } from '@angular/core';
import { ODataServiceFactory, ODataService, ODataPagedResult } from 'angular-odata-es5';
import { Observable } from 'rxjs';
import { IImage } from '../Utils/interfaces';

@Injectable()
export class GalleryOdataService {

  private odata: ODataService<IImage>;
  constructor(private odataFactory: ODataServiceFactory) {
    this.odata = this.odataFactory.CreateService<IImage>('/Images');
  }

  getImages(skip: number = 0,
            top: number = 30,
            query: string = '',
            orderBy: string = 'Company desc')
            : Observable<ODataPagedResult<IImage>> {
    return this.odata
            .Query()
            .Top(top)
            .Skip(skip)
            .OrderBy(orderBy)
            .Filter(query)
            .Select('ListingID,Company,Image_List')
            .ExecWithCount();

  }
}
