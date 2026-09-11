import { Routes } from '@angular/router';
import { HomePage } from './pages/home-page/home-page';
import { LoginPage } from './pages/login-page/login-page';
import { authGuard } from './services/guards/auth-guard-guard';
import { UserManagementPage } from './pages/user-management-page/user-management-page';
import { userResolver } from './resolvers/user-resolver';
import { authAdminGuard } from './services/guards/auth-admin-guard';
import { AdministrationPage } from './pages/administration-page/administration-page';
import { organisationResolver } from './resolvers/organisation-resolver';
import { RoomResolver } from './resolvers/room-resolver';
import { BookingPage } from './pages/booking-page/booking-page';
import { bookingResolver } from './resolvers/booking-resolver';

export const routes: Routes = [
  {
    path: 'home',
    component: HomePage,
    canActivate: [authGuard],
    resolve: { organisations: organisationResolver, rooms: RoomResolver },
  },
  {
    path: '',
    component: LoginPage,
  },
  {
    path: 'users',
    component: UserManagementPage,
    canActivate: [authAdminGuard],
    resolve: { users: userResolver },
  },
  {
    path: 'administration',
    component: AdministrationPage,
    canActivate: [authAdminGuard],
    resolve: { organisations: organisationResolver, rooms: RoomResolver },
  },
  {
    path: 'booking',
    component: BookingPage,
    canActivate: [authGuard],
    resolve: { bookings: bookingResolver },
  },
];
