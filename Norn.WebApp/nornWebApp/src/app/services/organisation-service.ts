import { inject, Injectable } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { NornApi } from './norn-api';
import { CreateOrganisationModel } from '../models/create-organisation-model';
import { BaseService } from './base-service';
import { OrganisationModel } from '../models/organisation-model';

@Injectable({
  providedIn: 'root',
})
export class OrganisationService extends BaseService {
  public override apiPath: string = '/Organisation';
  private readonly api = inject(NornApi);

  public async createOrginsation(organisation: CreateOrganisationModel) {
    return firstValueFrom(
      this.api.post<boolean, CreateOrganisationModel>(this.apiPath, organisation),
    );
  }

  public async updateOrganisation(organisation: CreateOrganisationModel) {
    return firstValueFrom(this.api.put<any, CreateOrganisationModel>(this.apiPath, organisation));
  }

  public async getAllOrganisations() {
    return firstValueFrom(this.api.get<OrganisationModel[]>(this.apiPath));
  }
}
