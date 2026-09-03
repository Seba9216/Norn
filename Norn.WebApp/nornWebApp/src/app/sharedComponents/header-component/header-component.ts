import { Component } from '@angular/core';
import { AuthService } from '../../services/auth-service';
import { inject } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header-component',
  imports: [],
  templateUrl: './header-component.html',
})
export class HeaderComponent {
  private authService = inject(AuthService);
  private router = inject(Router);
  isLogOutVisble() {
    return this.authService.isAuthenticated();
  }
  logOut() {
    this.authService.logout();
    this.router.navigate(['/']);
  }
  isAdmin(): boolean {
    return this.authService.isAdministrator();
  }
}
