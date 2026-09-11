import { BookingStatus } from './booking-status';
import { RoomsRelationBooking } from './rooms-relation';
import { TimeInterval } from './time-interval';
import { UserRelation } from './user-relation';

export class BookingModel {
  public id: number = 0;
  public room: RoomsRelationBooking | null = null;
  public timeInterval: TimeInterval | null = null;
  public user: UserRelation | null = null;
  public bookingStatus: BookingStatus | null = null;

  constructor(data?: Partial<BookingModel>) {
    Object.assign(this, data);
  }
}
