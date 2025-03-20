import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environments } from '../../../environments/environments';
import { HistoryRecord } from '../models/history.model';
import { Response } from '../models/api.model';

@Injectable({
  providedIn: 'root'
})
export class HistoryService {

  private _url = environments.apiURL;
  private _http = inject(HttpClient);

  getHistoryByRow(id: number) {
    return this._http.get<Response<HistoryRecord[]>>(`${this._url}${environments.endpoints.history}/${id}`);
  }

}
