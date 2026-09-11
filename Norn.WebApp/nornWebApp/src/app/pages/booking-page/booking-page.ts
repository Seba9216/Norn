import { ChangeDetectorRef, Component, inject, OnInit } from '@angular/core';
import { BookingModel } from '../../models/booking-model';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from '../../services/auth-service';
import { BookingService } from '../../services/booking-service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-booking-page',
  imports: [CommonModule],
  templateUrl: './booking-page.html',
})
export class BookingPage implements OnInit {
  route = inject(ActivatedRoute);
  authService = inject(AuthService);
  bookingService = inject(BookingService);
  cdr = inject(ChangeDetectorRef);

  public bookings: BookingModel[] = [];
  async ngOnInit() {
    this.bookings = this.route.snapshot.data['bookings'] as BookingModel[];
  }

  isAdmin(): boolean {
    return this.authService.isAdministrator();
  }
  isBooked(booking : BookingModel) : boolean{
    return booking.bookingStatus?.currentStatus === "Awaiting"
  }
  isCancelled(booking : BookingModel) : boolean{
    return booking.bookingStatus?.currentStatus !== "Cancelled"
  }
  async ApproveBooking(booking : BookingModel){
    
    await this.bookingService.approveBookingRequest(booking.id)
    await this.updateList();
  }
  async CancelBooking(booking : BookingModel){
    await this.bookingService.cancelBookingRequest(booking.id)
    await this.updateList();
  }
   async updateList() {
    this.bookings = await this.bookingService.GetAllBookings();
    this.cdr.detectChanges();

  }
  
}
