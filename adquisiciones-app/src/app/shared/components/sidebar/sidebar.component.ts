import { ChangeDetectionStrategy, Component } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-sidebar',
  imports: [ RouterLink ],
  standalone: true,
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class SidebarComponent {
  menuItems = [
    { name: 'Inicio', path: '/admin' },
    { name: 'Registrar Adquisición', path: '/admin/acquisition/register' },
    { name: 'Consultar Adquisiciones', path: '/admin/acquisition/tracing' }
  ];
}
