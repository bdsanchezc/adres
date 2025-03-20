import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { AuthService } from '../../../shared/services/auth.service';
import { HeaderComponent } from "../../../shared/components/header/header.component";
import { SidebarComponent } from "../../../shared/components/sidebar/sidebar.component";

@Component({
  selector: 'app-admin',
  imports: [RouterOutlet, HeaderComponent, SidebarComponent],
  standalone: true,
  templateUrl: './admin.component.html',
  styleUrl: './admin.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AdminComponent {

  private _auth = inject(AuthService);

  userName: string | null = null;
  userEmail: string | null = null;

  ngOnInit(): void {
    const user = this._auth.getUser();
    if (user) {
      this.userName = user.name;
      this.userEmail = user.email;
    }
  }

}
