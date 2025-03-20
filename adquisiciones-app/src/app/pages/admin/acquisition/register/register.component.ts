import { ChangeDetectionStrategy, ChangeDetectorRef, Component, inject } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { LoaderService } from '../../../../shared/services/loader.service';
import { finalize, forkJoin } from 'rxjs';
import { ParametricsService } from '../../../../shared/services/parametrics.service';
import { Parametrics } from '../../../../shared/models/parametrics';
import { CommonModule } from '@angular/common';
import Swal from 'sweetalert2'
import { RESPONSE_CONST } from '../../../../shared/constants/response.constants';
import { CurrencyFormatDirective } from '../../../../shared/directives/currency-format.directive';
import { CurrencyPipe } from '@angular/common';
import { AcquisitionForm, AcquisitionRequest } from '../../../../shared/models/acquisition.model';
import { AcquisitionsService } from '../../../../shared/services/acquisitions.service';
import { ActivatedRoute } from '@angular/router';
import { FormType } from '../../../../shared/models/global.model';

@Component({
  selector: 'app-register',
  imports: [ CommonModule, ReactiveFormsModule, CurrencyFormatDirective ],
  standalone: true,
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class RegisterComponent {
  private _loader = inject(LoaderService);
  private _cdr = inject(ChangeDetectorRef);
  private _parametrics = inject(ParametricsService);
  private _acquisitions = inject(AcquisitionsService);
  private _route = inject(ActivatedRoute);

  private _texts = RESPONSE_CONST;

  // Form
  type: FormType = 'New';
  form!: FormGroup<AcquisitionForm>;
  acquisitionId?: number;
  units: Parametrics[] = [];
  providers: Parametrics[] = [];
  states: Parametrics[] = [];

  ngOnInit(): void {
    this._buildForm();
    this._loadParametrics();
    this._listenerChanges();
    this._validateTypeForm();
  }

  private _buildForm(): void {
    this.form = new FormGroup<AcquisitionForm>({
      budget: new FormControl<number | null>(null, {
        nonNullable: true,
        validators: [Validators.required, Validators.min(1)]
      }),

      unit: new FormControl<number | null>(null, {
        nonNullable: true,
        validators: [Validators.required]
      }),

      service: new FormControl<string | null>(null, {
        nonNullable: true,
        validators: [Validators.required, Validators.minLength(3)]
      }),

      quantity: new FormControl<number | null>(null, {
        nonNullable: true,
        validators: [Validators.required, Validators.min(1)]
      }),

      unitValue: new FormControl<number | null>(null, {
        nonNullable: true,
        validators: [Validators.required, Validators.min(1)]
      }),

      totalValue: new FormControl<number | null>(null, {
        nonNullable: true,
        validators: [Validators.required, Validators.min(1)]
      }),

      date: new FormControl<string | null>(null, {
        nonNullable: true,
        validators: [Validators.required]
      }),

      provider: new FormControl<number | null>(null, {
        nonNullable: true,
        validators: [Validators.required]
      }),

      state: new FormControl<number | null>(null, {
        nonNullable: true,
        validators: [Validators.required]
      }),

      docs: new FormControl<string | null>(null)
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
        this._showAlert('error', this._texts.titleError, this._texts.messageErrorParametrics);
      },
    })
  }

  private _listenerChanges() {
    this.form.get('quantity')?.valueChanges.subscribe(() => this._updateTotalValue());
    this.form.get('unitValue')?.valueChanges.subscribe(() => this._updateTotalValue());
  }

  private _validateTypeForm() {
    this._route.paramMap.subscribe(params => {
      const id = params.get('id');
      if (!id) {
        this.type = 'New';
        this.form.get('state')?.clearValidators();
        this.form.get('state')?.updateValueAndValidity();
        return;
      }

      this.type = 'Edit';
      this.acquisitionId = Number(id);
      this._loadData(id);
    });
  }

  private _loadData(id: string) {
    this._loader.show();

    this._acquisitions.getRegisterById(id)
      .pipe(
        finalize(() => {
          this._loader.hide();
          this._cdr.detectChanges();
        })
      )
      .subscribe({
        next: (response) => {
          const formValues = {
            budget: response.data.presupuesto,
            unit: response.data.unidad?.id,
            service: response.data.tipoBienServicio,
            quantity: response.data.cantidad,
            unitValue: response.data.valorUnitario,
            totalValue: response.data.valorTotal,
            date: response.data.fechaAdquisicion,
            provider: response.data.proveedor?.id,
            state: response.data.estado?.id,
            docs: response.data.documentacion
          };

          this.form.patchValue(formValues);
        },
        error: (err) => {
          this._showAlert('error', this._texts.titleError, this._texts.errorPatchForm);
        }
      })
  }

  private _updateTotalValue(): void {
    const currencyPipe = new CurrencyPipe('en-US');

    const quantity = this.form.get('quantity')?.value ?? 0;
    let unitValue: number | string = this.form.get('unitValue')?.value ?? 0;

    if (typeof unitValue !== 'number') {
      unitValue = (unitValue as string).replace(/[^0-9.]/g, '');
      unitValue = parseFloat(unitValue) || 0;
    }

    const total = quantity * unitValue;

    this.form.get('totalValue')?.setValue(total, { emitEvent: false, onlySelf: true });

    setTimeout(() => {
      const formattedTotal = currencyPipe.transform(total, '', '', '1.0-0') || '';
      (document.querySelector('[formControlName="totalValue"]') as HTMLInputElement).value = formattedTotal;
    });
  }

  onSubmit() {
    this._loader.show();

    if( this.form.invalid ) {
      this._loader.hide();
      this._showAlert('warning', this._texts.titleError, this._texts.formInvalid);
      return;
    }

    const formData: AcquisitionRequest = {
      presupuesto: this.form.get('budget')?.value ?? 0,
      unidadId: this.form.get('unit')?.value ?? 0,
      tipoBienServicio: this.form.get('service')?.value ?? '',
      cantidad: this.form.get('quantity')?.value ?? 0,
      valorUnitario: this.form.get('unitValue')?.value ?? 0,
      valorTotal: this.form.get('totalValue')?.value ?? 0,
      fechaAdquisicion: this.form.get('date')?.value ?? '',
      proveedorId: this.form.get('provider')?.value ?? 0,
      documentacion: this.form.get('docs')?.value ?? '',
      ...(this.acquisitionId ? { estadoId: this.form.get('state')?.value ?? 0 } : {})
    };

    const request$ = this.acquisitionId
      ? this._acquisitions.putRegister(this.acquisitionId.toString(), formData)
      : this._acquisitions.createRegister(formData);

    request$
      .pipe(
        finalize(() => {
          this._loader.hide();
          this._cdr.detectChanges();
        })
      )
      .subscribe({
        next: () => {
          if( !this.acquisitionId ) {
            this.form.reset();
          }

          this._showAlert(
            'success',
            this.acquisitionId ? this._texts.titleEditSuccess : this._texts.titleRegisterSuccess,
            this.acquisitionId ? this._texts.messageEditSuccess : this._texts.messageRegisterSuccess
          );
        },
        error: () => {
          this._showAlert('error', this._texts.titleError, this._texts.errorRegister);
        }
      });
  }

  private _showAlert(icon: 'success' | 'error' | 'warning', title: string, text: string) {
    Swal.fire({
      title,
      text,
      icon,
      heightAuto: false,
      confirmButtonText: 'Entendido'
    });
  }

}
