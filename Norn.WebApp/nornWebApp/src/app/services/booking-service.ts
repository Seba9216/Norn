import { inject, Injectable } from '@angular/core';
import { BaseService } from './base-service';
import { NornApi } from './norn-api';
import { CreateBookingRequest } from '../models/create-booking-request';
import { BookingModel } from '../models/booking-model';
import { firstValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class BookingService extends BaseService {
  public override apiPath: string = '/Booking';
  private readonly api = inject(NornApi);

  public async createBooking(bookingRequest: CreateBookingRequest) {
    return firstValueFrom(
      this.api.post<BookingModel, CreateBookingRequest>(this.apiPath, bookingRequest),
    );
  }
  public async GetAllBookings() {
    return firstValueFrom(this.api.get<BookingModel[]>(this.apiPath));
  }
  public async GetAllBookingsForUser(email: string) {
    return firstValueFrom(this.api.get<BookingModel[]>(this.apiPath + '/ByMail/' + email));
  }
  public async approveBookingRequest(Id : number){
    return firstValueFrom(this.api.put<BookingModel,number>(this.apiPath + '/approve' ,Id))
  }
  public async cancelBookingRequest(Id : number){
        return firstValueFrom(this.api.put<BookingModel,number>(this.apiPath + '/cancel' ,Id))

  }
}
