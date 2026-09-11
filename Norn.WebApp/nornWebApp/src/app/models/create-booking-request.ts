export class CreateBookingRequest {
  public userId: number = 0;
  public roomId: number = 0;
  public timeIntervalId: number = 0;
  constructor(data?: Partial<CreateBookingRequest>) {
    Object.assign(this, data);
  }
}
