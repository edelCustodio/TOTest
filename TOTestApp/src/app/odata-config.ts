import { Injectable, Inject } from '@angular/core';
import { HttpResponse } from '@angular/common/http';
import { ODataConfiguration, IODataResponseModel } from 'angular-odata-es5';
import { AppConfig } from '../config/app.config.interface';
import { APP_CONFIG } from '../config/app.config';

interface MyIODataResponseModel<T> extends IODataResponseModel<T> {
    d: MyIODataResults<T>;
}

interface MyIODataResults<T> {
    results: T[];
}

@Injectable()
export class ODataConfig extends ODataConfiguration {
    baseUrl = this.config.ODATA_URL;

    constructor(
        @Inject(APP_CONFIG) private config: AppConfig) {
        super();
    }

    extractQueryResultData<T>(res: HttpResponse<MyIODataResponseModel<T>>): T[] {
        if (res.status < 200 || res.status >= 300) {
            throw new Error('Bad response status: ' + res.status);
        }
        return (res && res.body && res.body.value) as T[];
    }
}
