import { ChangeDetectionStrategy, ChangeDetectorRef, Component, effect, inject, Signal } from '@angular/core';
import { HistoryService } from '../../services/history.service';
import { ModalService } from '../../services/modal.service';
import { LoaderService } from '../../services/loader.service';
import { finalize } from 'rxjs';
import { CommonModule } from '@angular/common';
import { AcquisitionResponse } from '../../models/acquisition.model';
import { HistoryRecord } from '../../models/history.model';

@Component({
  selector: 'app-modal',
  imports: [CommonModule],
  templateUrl: './modal.component.html',
  styleUrl: './modal.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ModalComponent {
  private _modal = inject(ModalService);
  private _loader = inject(LoaderService);
  private _history = inject(HistoryService);
  private _cdr = inject(ChangeDetectorRef);

  modalOpen: Signal<boolean> = this._modal.modalOpen;
  acquisition: Signal<AcquisitionResponse | null> = this._modal.acquisition;

  history: HistoryRecord[] = [];
  historyPaginated: HistoryRecord[] = [];
  pageSize = 5;
  currentPage = 1;
  totalPages = 1;
  pages: number[] = [];

  constructor() {
    effect(() => {
      if (this.modalOpen()) {
        this._loadData();
      }
    });
  }

  private _loadData() {
    this._loader.show();
    const id = this.acquisition()?.id;
    if (!id) return;

    this._history.getHistoryByRow(id)
      .pipe(
        finalize(() => {
          this._loader.hide();
          this._cdr.detectChanges();
        })
      )
      .subscribe({
        next: (response) => {
          this.history = response.data;
          this.totalPages = Math.ceil(this.history.length / this.pageSize);
          this.updatePaginatedData();
          this.generatePages();
          this._cdr.detectChanges();
        },
        error: (err) => {
          console.log(err);
        }
      });
  }

  closeModal() {
    this._modal.closeModal();
  }

  // Pagination
  updatePaginatedData() {
    const start = (this.currentPage - 1) * this.pageSize;
    const end = start + this.pageSize;
    this.historyPaginated = this.history.slice(start, end);
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
