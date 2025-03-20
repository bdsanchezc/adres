import { Injectable, signal } from '@angular/core';
import { Auth } from '../models/auth.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private _userName = signal<string | null>(null);
  private _userEmail = signal<string | null>(null);

  constructor() { }

  setUser(name: string, email: string) {
    this._userName.set(name);
    this._userEmail.set(email);
  }

  getUser() {
    return {
      name: this._userName(),
      email: this._userEmail()
    }
  }

  logout() {
    this._userName.set(null);
    this._userEmail.set(null);
  }
}
