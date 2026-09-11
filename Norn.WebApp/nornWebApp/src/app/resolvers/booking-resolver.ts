import { inject } from '@angular/core';
import { ResolveFn } from '@angular/router';
import { AuthService } from '../services/auth-service';
import { BookingModel } from '../models/booking-model';
import { BookingService } from '../services/booking-service';

export const bookingResolver: ResolveFn<BookingModel[]> = (route, state) => {
  const authService = inject(AuthService);
  const bookingService = inject(BookingService);

  if (authService.isAdministrator()) {
    return bookingService.GetAllBookings();
  } else {
    const email = authService.getEmail();
    if (email !== null) {
      return bookingService.GetAllBookingsForUser(email);
    }
  }

  return [];
};
