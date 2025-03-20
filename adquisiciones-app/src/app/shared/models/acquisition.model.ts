import { FormControl } from '@angular/forms';
import { Parametrics } from './parametrics';

export interface AcquisitionForm {
  budget: FormControl<number | null>;
  unit: FormControl<number | null>;
  service: FormControl<string | null>;
  quantity: FormControl<number | null>;
  unitValue: FormControl<number | null>;
  totalValue: FormControl<number | null>;
  date: FormControl<string | null>;
  provider: FormControl<number | null>;
  state: FormControl<number | null>;
  docs?: FormControl<string | null>;
}

export interface FilterForm {
  unit: FormControl<string | null>;
  provider: FormControl<string | null>;
  state: FormControl<string | null>;
  date: FormControl<string | null>;
}

export interface AcquisitionRequest {
  presupuesto: number;
  unidadId: number;
  tipoBienServicio: string;
  cantidad: number;
  valorUnitario: number;
  valorTotal: number;
  fechaAdquisicion: string;
  proveedorId: number;
  documentacion: string;
  estadoId?: number;
}

export interface QueryParams {
  proveedorId: string;
  unidadId: string;
  estadoId: string;
  fechaAdquisicion: string;
}

export interface AcquisitionResponse {
  id: number;
  presupuesto: number;
  unidadId: number;
  unidad: Parametrics;
  tipoBienServicio: string;
  cantidad: number;
  valorUnitario: number;
  valorTotal: number;
  fechaAdquisicion: string;
  proveedorId: number;
  proveedor: Parametrics;
  documentacion: string;
  estadoId: number;
  estado: Parametrics;
}
