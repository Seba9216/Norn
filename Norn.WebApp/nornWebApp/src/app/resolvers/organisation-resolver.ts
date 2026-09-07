import { ResolveFn } from '@angular/router';
import { inject, runInInjectionContext } from '@angular/core';
import { OrganisationService } from '../services/organisation-service';
import { OrganisationModel } from '../models/organisation-model';

export const organisationResolver: ResolveFn<OrganisationModel[] | null> = (route, state) => {
  const organisationService = inject(OrganisationService);
  const result = organisationService.getAllOrganisations();
  return result;
};
