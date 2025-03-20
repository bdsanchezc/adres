import { ChangeDetectionStrategy, ChangeDetectorRef, Component, inject } from '@angular/core';
import { LoaderService } from '../../../../shared/services/loader.service';
import { ParametricsService } from '../../../../shared/services/parametrics.service';
import { AcquisitionsService } from '../../../../shared/services/acquisitions.service';
import { RESPONSE_CONST } from '../../../../shared/constants/response.constants';
import { finalize, forkJoin } from 'rxjs';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { Parametrics } from '../../../../shared/models/parametrics';
import { AcquisitionResponse, FilterForm, QueryParams } from '../../../../shared/models/acquisition.model';
import Swal from 'sweetalert2'
import { CommonModule } from '@angular/common';
import { HEADERS_TABLE_TRACING } from '../../../../shared/constants/global.constants';
import { RouterLink } from '@angular/router';
import { ModalService } from '../../../../shared/services/modal.service';

@Component({
  selector: 'app-tracing',
  imports: [ CommonModule, ReactiveFormsModule, RouterLink ],
  standalone: true,
  templateUrl: './tracing.component.html',
  styleUrl: './tracing.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class TracingComponent {
  private _loader = inject(LoaderService);
  private _cdr = inject(ChangeDetectorRef);
  private _parametrics = inject(ParametricsService);
  private _acquisitions = inject(AcquisitionsService);
  private _modal = inject(ModalService);

  private _texts = RESPONSE_CONST;

  // Form
  form!: FormGroup<FilterForm>;
  units: Parametrics[] = [];
  providers: Parametrics[] = [];
  states: Parametrics[] = [];

  // Table
  acquisitionsData: AcquisitionResponse[] = [];
  headers = HEADERS_TABLE_TRACING;
  paginatedData: AcquisitionResponse[] = [];
  pageSize = 5;
  currentPage = 1;
  totalPages = 1;
  pages: number[] = [];

  ngOnInit(): void {
    this._buildForm();
    this._loadParametrics();
  }

  private _buildForm(): void {
    this.form = new FormGroup<FilterForm>({
      unit: new FormControl<string | null>(null, { nonNullable: true }),
      date: new FormControl<string | null>(null, { nonNullable: true }),
      provider: new FormControl<string | null>(null, { nonNullable: true }),
      state: new FormControl<string | null>(null, { nonNullable: true }),
    });
  }

  private _loadParametrics() {
    this._loader.show();

    forkJoin({
      units: this._parametrics.getUnits(),
      providers: this._parametrics.getProviders(),
      states: this._parametrics.getStates(),
    })
    .pipe(
      finalize(() => {
        this._loader.hide();
        this._cdr.detectChanges();
      })
    )
    .subscribe({
      next: ({ units, providers, states }) => {
        this.units = units.data;
        this.providers = providers.data;
        this.states = states.data;
      },
      error: () => {
        Swal.fire({
          title: this._texts.titleError,
          text: this._texts.messageErrorParametrics,
          icon: 'error',
          heightAuto: false,
          confirmButtonText: 'Entendido'
        })
      },
    })
  }

  onReset() {
    this.form.reset();
  }

  onSubmit() {
    this._loader.show();
    const queries: QueryParams = {
      unidadId: this.form.get('unit')?.value ?? '',
      proveedorId: this.form.get('provider')?.value ?? '',
      estadoId: this.form.get('state')?.value ?? '',
      fechaAdquisicion: this.form.get('date')?.value ?? ''
    }

    this._acquisitions.getRegister(queries)
      .pipe(
        finalize(() => {
          this._loader.hide();
          this._cdr.detectChanges();
        })
      )
      .subscribe({
        next: (response) => {
          this.acquisitionsData = response.data;
          this.totalPages = Math.ceil(this.acquisitionsData.length / this.pageSize);
          this.updatePaginatedData();
          this.generatePages();
          this._cdr.detectChanges();
        },
        error: () => {
          Swal.fire({
            title: this._texts.titleError,
            text: this._texts.errorTracing,
            icon: 'error',
            heightAuto: false,
            confirmButtonText: 'Entendido'
          })
        }
      })
  }

  // Modal
  openModal(acquisition: AcquisitionResponse) {
    this._modal.openModal(acquisition);
  }

  // Pagination
  updatePaginatedData() {
    const start = (this.currentPage - 1) * this.pageSize;
    const end = start + this.pageSize;
    this.paginatedData = this.acquisitionsData.slice(start, end);
  }

  generatePages() {
    this.pages = Array.from({ length: this.totalPages }, (_, i) => i + 1);
  }

  nextPage() {
    if (this.currentPage < this.totalPages) {
      this.currentPage++;
      this.updatePaginatedData();
    }
  }

  prevPage() {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.updatePaginatedData();
    }
  }

  goToPage(page: number) {
    if (page >= 1 && page <= this.totalPages) {
      this.currentPage = page;
      this.updatePaginatedData();
    }
  }


}
