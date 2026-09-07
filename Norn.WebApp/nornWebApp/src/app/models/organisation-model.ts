import { RoomModel } from './room-model';

export class OrganisationModel {
  public id: number = 0;
  public name: string = '';
  public roomIds: Number[] = [];
  public rooms: RoomModel[] = [];
  constructor(data?: Partial<OrganisationModel>) {
    Object.assign(this, data);
  }
}
