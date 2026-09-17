import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth-service';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { SignalRService } from '../../services/signal-rservice';

@Component({
  selector: 'app-header-component',
  imports: [],
  templateUrl: './header-component.html',
})
export class HeaderComponent implements OnInit {
  private signalRService = inject(SignalRService);
  private authService = inject(AuthService);
  private router = inject(Router);

  ngOnInit(): void {
    this.signalRService.startConnection();
    this.signalRService.addBookingApprovedListener();
  }

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
