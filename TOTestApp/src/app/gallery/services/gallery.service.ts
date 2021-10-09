import { Inject, Injectable } from '@angular/core';
import { APP_CONFIG } from 'src/config/app.config';
import { AppConfig } from 'src/config/app.config.interface';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class GalleryService {

  constructor(
    private http: HttpClient,
    @Inject(APP_CONFIG) private config: AppConfig,
  ) { }
}
