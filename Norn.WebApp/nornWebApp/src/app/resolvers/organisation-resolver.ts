import { ResolveFn } from '@angular/router';
import { inject, runInInjectionContext } from '@angular/core';
import { OrganisationService } from '../services/organisation-service';
import { OrganisationModel } from '../models/organisation-model';
import { RoomModel } from '../models/room-model';
import { RoomService } from '../services/room-service';

export const organisationResolver: ResolveFn<OrganisationModel[] | null> = (route, state) => {
  const organisationService = inject(OrganisationService);
  const orgresult = organisationService.getAllOrganisations();
  return orgresult;
};
