export class TimeInterval {
  public roomId: number = 0;
  public from: Date = new Date();
  public to: Date = new Date();
  public isBooked: boolean = false;
  constructor(data?: Partial<TimeInterval>) {
    Object.assign(this, data);
  }
}
