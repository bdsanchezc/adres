import { Routes } from '@angular/router';
import { LoginComponent } from './pages/auth/login/login.component';
import { AdminComponent } from './config/layout/admin/admin.component';
import { HomeComponent } from './pages/admin/home/home.component';
import { RegisterComponent } from './pages/admin/acquisition/register/register.component';
import { TracingComponent } from './pages/admin/acquisition/tracing/tracing.component';

export const routes: Routes = [
  { path: 'auth', component: LoginComponent },
  {
    path: 'admin',
    component: AdminComponent,
    children: [
      { path: '', component: HomeComponent },
      { path: 'acquisition/register', component: RegisterComponent },
      { path: 'acquisition/register/:id', component: RegisterComponent },
      { path: 'acquisition/tracing', component: TracingComponent },
    ]
  },
  { path: '**', redirectTo: 'auth' },
];
