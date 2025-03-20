import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../../shared/services/auth.service';

@Component({
  selector: 'app-login',
  imports: [ FormsModule ],
  standalone: true,
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class LoginComponent {

  private _auth = inject(AuthService);
  private _router = inject(Router);

  name = '';
  email = '';

  login() {
    if (!this.name || !this.email) {
      alert('Login error');
      return;
    }

    this._auth.setUser(this.name, this.email);
    this._router.navigate(['/admin']);
  }
}
