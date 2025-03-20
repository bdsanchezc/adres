import { Injectable, Signal, signal } from '@angular/core';
import { AcquisitionResponse } from '../models/acquisition.model';

@Injectable({
  providedIn: 'root'
})
export class ModalService {
  private _open = signal<boolean>(false);
  private _acquisition = signal<AcquisitionResponse | null>(null);

  get modalOpen(): Signal<boolean> {
    return this._open
  }

  get acquisition(): Signal<AcquisitionResponse | null> {
    return this._acquisition;
  }

  openModal(acquisition: AcquisitionResponse) {
    this._acquisition.set(acquisition);
    this._open.set(true);
  }

  closeModal() {
    this._open.set(false);
  }

}
