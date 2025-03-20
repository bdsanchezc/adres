import { ChangeDetectionStrategy, Component, inject, Input } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  imports: [  ],
  standalone: true,
  templateUrl: './header.component.html',
  styleUrl: './header.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class HeaderComponent {
  @Input() userName: string | null = '';
  @Input() userEmail: string | null = '';

  private _auth = inject(AuthService);
  private _router = inject(Router);

  logout(): void {
    this._auth.logout();
    this._router.navigate(['/login']);
  }
}
