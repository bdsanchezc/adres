import { Parametrics } from "./parametrics";

export interface HistoryRecord {
  id: number;
  adquisicionId: number;
  campoModificado: string;
  valorAnterior: string;
  valorNuevo: string;
  fechaModificacion: string;
  usuario: string;
  accion: string;
}
