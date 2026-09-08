import { TimeLease } from '../enums/timeLease';

export class CreateOrUpdateRoomModel {
  public id: number = 0;
  public name: string = '';
  public monday: boolean = true;
  public tuesday: boolean = true;
  public wensday: boolean = true;
  public thursday: boolean = true;
  public friday: boolean = true;
  public saturday: boolean = true;
  public sunday: boolean = true;
  public timeLease: TimeLease = TimeLease.Hours;
  public increment: number = 0;
  public fromHour: number = 0;
  public toHour: number = 0;
  public organisationIds: number[] = [];

  constructor(data?: Partial<CreateOrUpdateRoomModel>) {
    Object.assign(this, data);
  }
}
