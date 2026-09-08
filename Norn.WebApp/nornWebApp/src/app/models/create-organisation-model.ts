export class CreateOrganisationModel {
  public id: number = 0;
  public name: string = '';
  public roomIds: number[] = [];

  constructor(data?: Partial<CreateOrganisationModel>) {
    Object.assign(this, data);
  }
}
