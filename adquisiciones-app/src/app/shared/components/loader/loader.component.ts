import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, ChangeDetectorRef, Component, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { LoaderService } from '../../services/loader.service';

@Component({
  selector: 'app-loader',
  imports: [CommonModule],
  standalone: true,
  templateUrl: './loader.component.html',
  styleUrl: './loader.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class LoaderComponent {
  isLoading!: Observable<boolean>;

  private _loader = inject(LoaderService);
  private _cdr = inject(ChangeDetectorRef);

  ngOnInit(): void {
    this.isLoading = this._loader.isLoading;
    this.isLoading.subscribe(() => {
      this._cdr.detectChanges();
    });
  }
}
