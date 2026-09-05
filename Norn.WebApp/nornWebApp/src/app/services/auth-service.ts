import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private jwtHelper = new JwtHelperService();

  get token(): string | null {
    return localStorage.getItem('token');
  }

  isAuthenticated(): boolean {
    const token = this.token;
    if (!token) {
      return false;
    }
    
    return !this.jwtHelper.isTokenExpired(token);
  }

  getRole(): string | null {
    const token = this.token;
    if (!token) {
      return null;
    }
    const decoded = this.jwtHelper.decodeToken(token);
    return decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
  }
  isAdministrator(): boolean {
    const currentRole = this.getRole();
    return currentRole === 'Admin';
  }
  logout() {
    localStorage.removeItem('token');
    this.isAuthenticated();
  }
}
