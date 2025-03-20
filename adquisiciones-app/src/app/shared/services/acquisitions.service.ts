import { inject, Injectable } from '@angular/core';
import { environments } from '../../../environments/environments';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { AcquisitionRequest, AcquisitionResponse, QueryParams } from '../models/acquisition.model';
import { Response } from '../models/api.model';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class AcquisitionsService {

  private _url = environments.apiURL;
  private _http = inject(HttpClient);
  private _auth = inject(AuthService);

  constructor() { }

  createRegister(data: AcquisitionRequest) {
    const headers = new HttpHeaders().set('usuario', this._auth.getUser().name  ?? '');
    return this._http.post<Response<AcquisitionResponse>>(`${this._url}${environments.endpoints.acquisitions}`, data, { headers });
  }

  getRegister(data: QueryParams) {
    const params = new HttpParams()
          .set('proveedorId', data.proveedorId)
          .set('unidadId', data.unidadId)
          .set('estadoId', data.estadoId)
          .set('fechaAdquisicion', data.fechaAdquisicion);

    return this._http.get<Response<AcquisitionResponse[]>>(`${this._url}${environments.endpoints.acquisitions}`, { params });
  }

  getRegisterById(id: string) {
    return this._http.get<Response<AcquisitionResponse>>(`${this._url}${environments.endpoints.acquisitions}/${id}`);
  }

  putRegister(id: string, data: AcquisitionRequest) {
    const headers = new HttpHeaders().set('usuario', this._auth.getUser().name  ?? '');
    return this._http.put(`${this._url}${environments.endpoints.acquisitions}/${id}`, data, { headers });
  }

}
