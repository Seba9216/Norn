import { CreateOrganisationModel } from '../../models/create-organisation-model';
import { RoomsRelation } from '../../models/rooms-relation';

export class CreateOrUpdateOrganisationData {
  public organisationToUpdate: CreateOrganisationModel | null = null;
  public rooms: RoomsRelation[] = [];
  public currentRoomsIds: number[] | null = null;
  constructor(data?: Partial<CreateOrUpdateOrganisationData>) {
    Object.assign(this, data);
  }
}
