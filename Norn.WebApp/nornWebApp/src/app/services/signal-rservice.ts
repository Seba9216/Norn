import { inject, Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { BookingModel } from '../models/booking-model';
import { AuthService } from './auth-service';
import { MatSnackBar } from '@angular/material/snack-bar';


@Injectable({
  providedIn: 'root',
})
export class SignalRService {
    private authService = inject(AuthService);
    private hubConnection: signalR.HubConnection;
    private snackBar: MatSnackBar

    public bookingConfirmed : BookingModel | null = null;
    private hubConnectionAdress : string = "http://localhost:8008/nornHub";
    constructor() {
      this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(this.hubConnectionAdress, {
        withCredentials : false,
        
      }) 
      .build(); 
      this.snackBar = new MatSnackBar();
    }

    public startConnection = () => {

    this.hubConnection
      .start()
      .then(() => console.log('Nornhub Connection started'))
      .catch(err => console.log('Error establishing SignalR connection: ' + err));
  }
  public addBookingApprovedListener = () => {
this.hubConnection.on('BookingApproved', (...args) => {
  const booking = args[0] as BookingModel;
  if(this.authService.getEmail() == booking!.user?.email){
      this.bookingConfirmed = booking;
      this.snackBar.open("Your booking at " + booking.room?.roomName +  " has been approved!","Close", {
         duration : 5000,
         horizontalPosition : "right",
         verticalPosition : "top"
      } )
  }else{
    this.bookingConfirmed = null;
  }
});
  }
}
