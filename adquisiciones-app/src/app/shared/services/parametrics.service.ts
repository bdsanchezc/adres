import { inject, Injectable } from '@angular/core';
import { environments } from '../../../environments/environments';
import { HttpClient } from '@angular/common/http';
import { Parametrics } from '../models/parametrics';
import { Response } from '../models/api.model';

@Injectable({
  providedIn: 'root'
})
export class ParametricsService {
  private _url = environments.apiURL;
  private _http = inject(HttpClient);

  constructor() {}

  getUnits() {
    return this._http.get<Response<Parametrics[]>>(`${this._url}${environments.endpoints.parametrics.units}`);
  }

  getStates() {
    return this._http.get<Response<Parametrics[]>>(`${this._url}${environments.endpoints.parametrics.states}`);
  }

  getProviders() {
    return this._http.get<Response<Parametrics[]>>(`${this._url}${environments.endpoints.parametrics.providers}`);
  }
}
