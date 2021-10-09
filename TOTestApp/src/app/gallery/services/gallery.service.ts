import { Inject, Injectable } from '@angular/core';
import { APP_CONFIG } from 'src/config/app.config';
import { AppConfig } from 'src/config/app.config.interface';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IImage } from '../Utils/interfaces';

@Injectable({
  providedIn: 'root'
})
export class GalleryService {

  private getImagesURL = `${this.config.API_URL}/Image`;

  constructor(
    private http: HttpClient,
    @Inject(APP_CONFIG) private config: AppConfig,
  ) { }

  getImages(): Observable<IImage[]> {
    return this.http.get<IImage[]>(this.getImagesURL);
  }
}
