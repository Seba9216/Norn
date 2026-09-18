import { AfterViewInit, ChangeDetectorRef, Component, inject, OnInit, ViewChild } from '@angular/core';
import { BookingModel } from '../../models/booking-model';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from '../../services/auth-service';
import { BookingService } from '../../services/booking-service';
import { CommonModule } from '@angular/common';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import {MatPaginator, MatPaginatorModule} from '@angular/material/paginator';

@Component({
  selector: 'app-booking-page',
  imports: [CommonModule, MatTableModule,MatPaginatorModule],
  templateUrl: './booking-page.html',
})
export class BookingPage implements OnInit,AfterViewInit {
  route = inject(ActivatedRoute);
  authService = inject(AuthService);
  bookingService = inject(BookingService);
  cdr = inject(ChangeDetectorRef);

  public displayedColumns = ['room', 'from', 'to', 'user', 'status', 'actions'];

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  public bookings: BookingModel[] = [];
  public dataSource = new MatTableDataSource<BookingModel>();

  async ngOnInit() {
    this.bookings = this.route.snapshot.data['bookings'] as BookingModel[];
    this.dataSource.data = this.bookings;
  }
  ngAfterViewInit(): void {
this.dataSource.paginator = this.paginator;
}

  isAdmin(): boolean {
    return this.authService.isAdministrator();
  }
  isBooked(booking: BookingModel): boolean {
    return booking.bookingStatus?.currentStatus === 'Awaiting';
  }
  isCancelled(booking: BookingModel): boolean {
    return booking.bookingStatus?.currentStatus === 'Cancelled';
  }
  async ApproveBooking(booking: BookingModel) {
    await this.bookingService.approveBookingRequest(booking.id);
    await this.updateList();
  }
  async CancelBooking(booking: BookingModel) {
    await this.bookingService.cancelBookingRequest(booking.id);
    await this.updateList();
  }
  async updateList() {
    if (this.isAdmin()) {
      this.bookings = await this.bookingService.GetAllBookings();
    } else {
      const email = this.authService.getEmail() as string;
      this.bookings = await this.bookingService.GetAllBookingsForUser(email);
    }
    this.dataSource.data = this.bookings;
    this.cdr.detectChanges();
  }
}
