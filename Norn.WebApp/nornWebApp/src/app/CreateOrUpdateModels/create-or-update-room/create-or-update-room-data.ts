import { CreateOrUpdateRoomModel } from '../../models/create-or-update-room-model';
import { OrganisationsRelation } from '../../models/organisations-relation';

export class CreateOrUpdateRoomData {
  public request: CreateOrUpdateRoomModel | null = null;
  public currentOrganisationIds: number[] = [];
  public organisations: OrganisationsRelation[] = [];
  constructor(data?: Partial<CreateOrUpdateRoomData>) {
    Object.assign(this, data);
  }
}
